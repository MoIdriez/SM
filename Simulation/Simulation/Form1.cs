using Simulation.Examples;
using Simulation.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulation
{
    public partial class Form1 : Form
    {
        static int counter = 0;
        static int interval = 240;
        static Timer timer = new Timer();
        static Planner planner = GetPlanner();

        public Form1()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(TimerEventProcessor);

            timer.Interval = interval;
            timer.Start();
        }

        private void TimerEventProcessor(object sender, EventArgs e)
        {
            if (counter > 150)
            {
                timer.Stop();
            } else
            {
                if (counter != planner.Steps.Count -1)
                {
                    counter++;
                    Refresh();
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            label1.Text = counter.ToString();
            var cameraLocation = planner.Steps[counter];
            ExpSimpleSlam.Run(e.Graphics, cameraLocation);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            timer.Stop();
            counter = 0;
            interval += interval;
            timer.Start();
        }

        private static Planner GetPlanner()
        {
            var planner = new Planner(new Engine.Point(600, 475), 315, 45);

            // move to area between rooms
            var betweenRooms = new Engine.Point(825, 475);
            planner.AddCourse(betweenRooms, 5);

            // rotate to room 1
            planner.AddRotation(-90, 10);

            // enter room 1
            var inRoom1 = new Engine.Point(825, 200);
            planner.AddCourse(inRoom1, 5);

            // rotate in room1
            planner.AddRotation(-180, 10);

            // enter room 2
            var inRoom2 = new Engine.Point(825, 700);
            planner.AddCourse(inRoom2, 15);

            return planner;
        }
    }
}
