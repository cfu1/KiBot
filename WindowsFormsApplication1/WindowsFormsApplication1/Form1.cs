using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Kinect;

using NKH.MindSqualls;
using NKH.MindSqualls.HiTechnic;

namespace WindowsFormsApplication1
{
    public enum Motion {Forward, TurnLeft, TurnRight, Back, Brake };


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        KinectSensor myKinect;

        List<SkeletonPoint> leftInitialPosition;
        List<SkeletonPoint> rightInitialPosition;
        private bool initialPositionReady = false;
        Dictionary<string, SkeletonPoint> initialPositions;

        string leftHandKey = "leftHand";
        string rightHandKey = "rightHand";

        const double THRESHOLD = 0.15; // threshold is 5cm.
        //List<Motion> cmds;
        Dictionary<Motion, bool> cmds;
        Dictionary<Motion, bool> nxtStatus;

        NKH.MindSqualls.NxtBrick nxt;
        NxtMotorSync motorPair;
        
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                myKinect = KinectSensor.KinectSensors[0];
                myKinect.SkeletonStream.Enable();
                myKinect.Start();
            }
            catch
            {
                MessageBox.Show("Kinect initialise failed", "camera view");
                Application.Exit();
            }

            leftInitialPosition = new List<SkeletonPoint>();
            rightInitialPosition = new List<SkeletonPoint>();
            initialPositions = new Dictionary<string, SkeletonPoint>();
            //cmds = new List<Motion>();
            cmds = new Dictionary<Motion, bool>();
            cmds[Motion.Back] = false;
            cmds[Motion.Brake] = false;
            cmds[Motion.Forward] = false;
            cmds[Motion.TurnRight] = false;
            cmds[Motion.TurnLeft] = false;

            nxtStatus = new Dictionary<Motion, bool>();
            nxtStatus[Motion.Brake] = false;
            nxtStatus[Motion.Back] = false;
            nxtStatus[Motion.Forward] = false;
            nxtStatus[Motion.TurnLeft] = false;
            nxtStatus[Motion.TurnRight] = false;

            nxt = new NxtBrick(NxtCommLinkType.Bluetooth, 3);
            nxt.MotorB = new NxtMotor();
            nxt.MotorC = new NxtMotor();
            motorPair = new NxtMotorSync(nxt.MotorB, nxt.MotorC);

            nxt.Connect();

            NXTLabel.Text = "NXT is connected.";

            myKinect.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(myKinect_SkeletonFrameReady);
            
        }

        private void myKinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
    
            Skeleton[] skeletons = null;

            using (SkeletonFrame frame = e.OpenSkeletonFrame())
            {
                if (frame != null)
                {
                    skeletons = new Skeleton[frame.SkeletonArrayLength];
                    frame.CopySkeletonDataTo(skeletons);
                }

                if (skeletons == null) return;

                foreach (Skeleton skeleton in skeletons)
                {
                    if (skeleton.TrackingState == SkeletonTrackingState.Tracked)
                    {
                        if (StatusLabel.Text != "Ready")
                        {
                            if (!initialPositionReady)
                                StatusLabel.Text = "Kinect is initialing, please holding hands in position";
                            else
                                StatusLabel.Text = "Ready";
                        }

                        Joint leftHand = skeleton.Joints[JointType.HandLeft];
                        Joint rightHand = skeleton.Joints[JointType.HandRight];

                        SkeletonPoint leftHandPosition = leftHand.Position;
                        SkeletonPoint rightHandPosition = rightHand.Position;

                        if (!initialPositionReady)
                        {
                            leftInitialPosition.Add(leftHandPosition);
                            rightInitialPosition.Add(rightHandPosition);

                            if (rightInitialPosition.Count == 100)
                            {
                                SkeletonPoint r = new SkeletonPoint();
                                SkeletonPoint l = new SkeletonPoint();

                                foreach (SkeletonPoint p in leftInitialPosition)
                                {
                                    l.X += p.X;
                                    l.Y += p.Y;
                                    l.Z += p.Z;
                                }

                                foreach (SkeletonPoint p in rightInitialPosition)
                                {
                                    r.X += p.X;
                                    r.Y += p.Y;
                                    r.Z += p.Z;
                                }

                                l.X /= leftInitialPosition.Count;
                                l.Y /= leftInitialPosition.Count;
                                l.Z /= leftInitialPosition.Count;
                                r.X /= rightInitialPosition.Count;
                                r.Y /= rightInitialPosition.Count;
                                r.Z /= rightInitialPosition.Count;

                                initialPositions[leftHandKey] = l;
                                initialPositions[rightHandKey] = r;

                                StatusLabel.Text = "Ready";

                                initialPositionReady = true;
                            }
                        }
                        else 
                        {
                            GestureCognition(leftHandPosition, rightHandPosition);
                            SendCommand();
                        }
                     
                        LeftHandLabel.Text = string.Format("Lefthand: X: {0: 0.00}, Y: {1: 0.00}, Z: {2: 0.00}", leftHandPosition.X, leftHandPosition.Y, leftHandPosition.Z);
                        RightHandLabel.Text = string.Format("Righthand: X: {0: 0.00}, Y: {1: 0.00}, Z: {2: 0.00}", rightHandPosition.X, rightHandPosition.Y, rightHandPosition.Z);
                    }
                }
            }
        }

        private void GestureCognition(SkeletonPoint leftHandPosition, SkeletonPoint rightHandPosition)
        {
            double leftXOff = leftHandPosition.X - initialPositions[leftHandKey].X - THRESHOLD;
            //double leftYOff = leftHandPosition.Y - initialPositions[leftHandKey].Y - THRESHOLD;
            double leftZOff = leftHandPosition.Z - initialPositions[leftHandKey].Z - THRESHOLD;
            double rightXOff = rightHandPosition.X - initialPositions[rightHandKey].X - THRESHOLD;
            //double rightYOff = rightHandPosition.X - initialPositions[rightHandKey].Y - THRESHOLD;
            double rightZOff = rightHandPosition.Z - initialPositions[rightHandKey].Z - THRESHOLD;

            LeftOffsetLabel.Text = string.Format("Lefthand: xoffset {0: 0.00}, zoffset: {1: 0.00}", leftXOff + THRESHOLD, leftZOff + THRESHOLD);
            RightOffsetLabel.Text = string.Format("Righthand: xoffset {0: 0.00}, zoffset: {1: 0.00}", rightXOff + THRESHOLD, rightZOff + THRESHOLD);

            if ((rightXOff <= 0 && rightXOff >= -2 * THRESHOLD) && (rightZOff <= 0 && rightZOff >= -2 * THRESHOLD))
            {
                cmds[Motion.Brake] = true;
                cmds[Motion.Forward]= false;
                cmds[Motion.Back] = false;
                cmds[Motion.TurnLeft] = false;
                cmds[Motion.TurnRight] = false;
                BrakeLabel.Text = "Braking";
                TurnLabel.Text = "";
                StraightLabel.Text = "";
            }
            //Tend to Turn Left
            if (leftXOff > 0)
            {
                if (rightXOff > -2 * THRESHOLD)
                {
                    if (rightZOff <= 0 && rightZOff >= -2 * THRESHOLD)
                    {
                        cmds[Motion.TurnRight] = true;
                        cmds[Motion.Brake] = false;
                        cmds[Motion.TurnLeft] = false;
                        cmds[Motion.Forward] = false;
                        cmds[Motion.Back] = false;
                        TurnLabel.Text = "Turn to Right";
                    }
                    else if (rightZOff < -2 * THRESHOLD)
                    {
                        cmds[Motion.TurnRight] = true;
                        cmds[Motion.Brake] = false;
                        cmds[Motion.TurnLeft] = false;
                        cmds[Motion.Forward] = true;
                        cmds[Motion.Back] = false;
                        TurnLabel.Text = "Forward and Turn to Right";
                    }
                    else 
                    {
                        cmds[Motion.TurnRight] = true;
                        cmds[Motion.Brake] = false;
                        cmds[Motion.TurnLeft] = false;
                        cmds[Motion.Forward] = false;
                        cmds[Motion.Back] = true;
                        TurnLabel.Text = "Backward and Turn to Right";
                    }               
                }
                else 
                {
                    TurnLabel.Text = "";
                    BrakeLabel.Text = "Braking";
                    cmds[Motion.Brake] = true;
                    cmds[Motion.Forward] = false;
                    cmds[Motion.Back] = false;
                    cmds[Motion.TurnLeft] = false;
                    cmds[Motion.TurnRight] = false;               
                }

                BrakeLabel.Text = "";
            }
            else if (rightXOff < -2 * THRESHOLD)
            {
                if (rightZOff <= 0 && rightZOff >= -2 * THRESHOLD)
                {
                    cmds[Motion.TurnRight] = false;
                    cmds[Motion.Brake] = false;
                    cmds[Motion.TurnLeft] = true;
                    cmds[Motion.Forward] = false;
                    cmds[Motion.Back] = false;
                    TurnLabel.Text = "Turn to Left";
                }
                else if (rightZOff < -2 * THRESHOLD)
                {
                    cmds[Motion.TurnRight] = false;
                    cmds[Motion.Brake] = false;
                    cmds[Motion.TurnLeft] = true;
                    cmds[Motion.Forward] = true;
                    cmds[Motion.Back] = false;
                    TurnLabel.Text = "Forward and Turn to Left";
                }
                else
                {
                    cmds[Motion.TurnRight] = false;
                    cmds[Motion.Brake] = false;
                    cmds[Motion.TurnLeft] = true;
                    cmds[Motion.Forward] = false;
                    cmds[Motion.Back] = true;
                    TurnLabel.Text = "Backward and Turn to Left";
                }

                BrakeLabel.Text = "";
            }    
            else if (rightZOff < -2 * THRESHOLD)
            {
                cmds[Motion.TurnRight] = false;
                cmds[Motion.Brake] = false;
                cmds[Motion.TurnLeft] = false;
                cmds[Motion.Forward] = true;
                cmds[Motion.Back] = false;
                StraightLabel.Text = "Go Forward";
                BrakeLabel.Text = "";
            }
            else if (rightZOff > 0)
            {
                cmds[Motion.TurnRight] = false;
                cmds[Motion.Brake] = false;
                cmds[Motion.TurnLeft] = false;
                cmds[Motion.Forward] = false;
                cmds[Motion.Back] = true;
                StraightLabel.Text = "Go Backward";
                BrakeLabel.Text = "";
            }
        }

        private void SendCommand()
        {
            int cmdFlag = 0;

            if (cmds[Motion.Forward])
                cmdFlag += 1;
            if (cmds[Motion.Back])
                cmdFlag += 2;
            if (cmds[Motion.TurnLeft])
                cmdFlag += 4;
            if (cmds[Motion.TurnRight])
                cmdFlag += 8;

            int statusFlag = 0;

            if (nxtStatus[Motion.Forward])
                statusFlag += 1;
            if (nxtStatus[Motion.Back])
                statusFlag += 2;
            if (nxtStatus[Motion.TurnLeft])
                statusFlag += 4;
            if (nxtStatus[Motion.TurnRight])
                statusFlag += 8;

            switch (cmdFlag)
            {
                case 1:
                    if (statusFlag != 1)
                        Forward();
                    break;
                case 2:
                    if (statusFlag != 2)
                        Backward();
                    break;
                case 4:
                    if (statusFlag != 4)
                        TurnLeft();
                    break;
                case 5:
                    if(statusFlag != 5)
                        ForwardAndLeft();
                    break;
                case 6:
                    if(statusFlag != 6)
                        BackwardAndLeft();
                    break;
                case 8:
                    if(statusFlag != 8)
                        TurnRight();
                    break;
                case 9:
                    if(statusFlag != 9)
                        ForwardAndRight();
                    break;
                case 10:
                    if(statusFlag != 10)
                        BackwardAndRight();
                    break;
                default:
                    Brake();
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (myKinect != null)
                myKinect.Stop();
        }
        


        #region Send Commands to NXT 
        private void Brake()
        {
            textBox1.Text += "Stop" + Environment.NewLine;
            
            if (motorPair != null)
                motorPair.Brake();
            else
            {
                if(nxt.MotorB != null)
                    nxt.MotorB.Brake();
                if(nxt.MotorC != null)
                    nxt.MotorC.Brake();
            }

            nxtStatus[Motion.Forward] = false;
            nxtStatus[Motion.Back] = false;
            nxtStatus[Motion.TurnLeft] = false;
            nxtStatus[Motion.TurnRight] = false;
            nxtStatus[Motion.Brake] = true;
        }

        private void Forward()
        {
            textBox1.Text += "Forward" + Environment.NewLine;

            if (motorPair != null)
                motorPair.Brake();
            nxt.MotorB = new NxtMotor();
            nxt.MotorC = new NxtMotor();
            nxt.MotorB.ResetMotorPosition(true);
            nxt.MotorC.ResetMotorPosition(true);
            motorPair = new NxtMotorSync(nxt.MotorB, nxt.MotorC);
            motorPair.Run(100, 0, 0);

            nxtStatus[Motion.Forward] = true;
            nxtStatus[Motion.Back] = false;
            nxtStatus[Motion.TurnLeft] = false;
            nxtStatus[Motion.TurnRight] = false;
            nxtStatus[Motion.Brake] = false;
        }

        private void Backward()
        {
            textBox1.Text += "Backward" + Environment.NewLine;

            if (motorPair != null)
                motorPair.Brake();
            nxt.MotorB = new NxtMotor(true);
            nxt.MotorC = new NxtMotor(true);
            nxt.MotorB.ResetMotorPosition(true);
            nxt.MotorC.ResetMotorPosition(true);
            motorPair = new NxtMotorSync(nxt.MotorB, nxt.MotorC);
            motorPair.Run(100, 0, 0);

            nxtStatus[Motion.Forward] = false;
            nxtStatus[Motion.Back] = true;
            nxtStatus[Motion.TurnLeft] = false;
            nxtStatus[Motion.TurnRight] = false;
            nxtStatus[Motion.Brake] = false;
        }

        private void TurnLeft()
        {
            textBox1.Text += "Turn Left" + Environment.NewLine;

            if (motorPair != null)
                motorPair.Brake();
            nxt.MotorB = new NxtMotor();
            nxt.MotorC = new NxtMotor();
            nxt.MotorB.Run(100, 0);
            nxt.MotorC.Run(-100, 0);

            nxtStatus[Motion.Forward] = false;
            nxtStatus[Motion.Back] = false;
            nxtStatus[Motion.TurnLeft] = true;
            nxtStatus[Motion.TurnRight] = false;
            nxtStatus[Motion.Brake] = false;
        }

        private void TurnRight()
        {
            textBox1.Text += "Turn Right" + Environment.NewLine;

            if (motorPair != null)
                motorPair.Brake();
            nxt.MotorB = new NxtMotor();
            nxt.MotorC = new NxtMotor();
            nxt.MotorB.Run(-100, 0);
            nxt.MotorC.Run(100, 0);

            nxtStatus[Motion.Forward] = false;
            nxtStatus[Motion.Back] = false;
            nxtStatus[Motion.TurnLeft] = false;
            nxtStatus[Motion.TurnRight] = true;
            nxtStatus[Motion.Brake] = false;
        }

        private void ForwardAndLeft()
        {
            textBox1.Text += "Forward And Left" + Environment.NewLine;

            if (motorPair != null)
                motorPair.Brake();
            nxt.MotorB = new NxtMotor();
            nxt.MotorC = new NxtMotor();
            nxt.MotorB.Run(100, 0);
            nxt.MotorC.Run(50, 0);

            nxtStatus[Motion.Forward] = true;
            nxtStatus[Motion.Back] = false;
            nxtStatus[Motion.TurnLeft] = true;
            nxtStatus[Motion.TurnRight] = false;
            nxtStatus[Motion.Brake] = false;
        }

        private void ForwardAndRight()
        {
            textBox1.Text += "Forward And Right" + Environment.NewLine;

            if (motorPair != null)
                motorPair.Brake();
            nxt.MotorB = new NxtMotor();
            nxt.MotorC = new NxtMotor();
            nxt.MotorB.Run(50, 0);
            nxt.MotorC.Run(100, 0);

            nxtStatus[Motion.Forward] = true;
            nxtStatus[Motion.Back] = false;
            nxtStatus[Motion.TurnLeft] = false;
            nxtStatus[Motion.TurnRight] = true;
            nxtStatus[Motion.Brake] = false;
        }

        private void BackwardAndLeft()
        {
            textBox1.Text += "Backward And Left" + Environment.NewLine;

            if (motorPair != null)
                motorPair.Brake();
            nxt.MotorB = new NxtMotor(true);
            nxt.MotorC = new NxtMotor(true);
            nxt.MotorB.Run(100, 0);
            nxt.MotorC.Run(50, 0);

            nxtStatus[Motion.Forward] = false;
            nxtStatus[Motion.Back] = true;
            nxtStatus[Motion.TurnLeft] = true;
            nxtStatus[Motion.TurnRight] = false;
            nxtStatus[Motion.Brake] = false;
        }

        private void BackwardAndRight()
        {
            textBox1.Text += "Backward And Right" + Environment.NewLine;

            if (motorPair != null)
                motorPair.Brake();
            nxt.MotorB = new NxtMotor(true);
            nxt.MotorC = new NxtMotor(true);
            nxt.MotorB.Run(50, 0);
            nxt.MotorC.Run(100, 0);

            nxtStatus[Motion.Forward] = false;
            nxtStatus[Motion.Back] = true;
            nxtStatus[Motion.TurnLeft] = false;
            nxtStatus[Motion.TurnRight] = true;
            nxtStatus[Motion.Brake] = false;
        }
        #endregion


        private void button2_Click(object sender, EventArgs e)
        {
            Brake();
            nxt.Disconnect();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (motorPair != null)
                    motorPair.Brake();
                else
                {
                    if (nxt.MotorB != null)
                        nxt.MotorB.Brake();
                    if (nxt.MotorC != null)
                        nxt.MotorC.Brake();
                }
            }
            catch (Exception ex) {
                e.Cancel = false;
            }          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (nxt.MotorB != null)
                nxt.MotorB.Idle();
            if (nxt.MotorC != null)
                nxt.MotorC.Idle();
        }

      
    }
}
