using Engine;
using Simulation.Examples;
using Simulation.Helpers;
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
        static Random random = new Random();
        static Planner planner = GetPlanner();
        static List<Engine.Point> map = new List<Engine.Point>();

        public Form1()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(TimerEventProcessor);

            timer.Interval = interval;
            timer.Start();
        }

        private void TimerEventProcessor(object sender, EventArgs e)
        {
            if (counter > 250)
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
            TestNoise();
            label1.Text = counter.ToString();
            var cameraLocation = planner.Steps[counter];

            Scene scene = ExpSimpleSlam.Run(e.Graphics, cameraLocation);
            DrawMiniMap(e.Graphics, scene);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            timer.Stop();
            counter = 0;
            interval += interval;
            timer.Start();
        }

        private void DrawMiniMap(Graphics g, Scene scene)
        {
            map.AddRange(scene.GetExpectedIntersectingPoints());
            map.ForEach(p => new Circle(p / 5, 2).Draw(g));
        }

        private static void TestNoise()
        {
            var noise = new List<double>();
            for (int i = 0; i < 150; i++)
                noise.Add(Eq.SampleGaussian(random, 0, 150));

            var noise1 = new List<double>();
            for (int i = 0; i < 150; i++)
                noise1.Add(Eq.SampleGaussian(random, -20, 150));

            var noise2 = new List<double>();
            for (int i = 0; i < 150; i++)
                noise2.Add(Eq.SampleGaussian(random, 0, -30));

            var noise3 = new List<double>();
            for (int i = 0; i < 150; i++)
                noise3.Add(Eq.SampleGaussian(random, 20, 30));
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

            // rotate out of room
            planner.AddRotation(180, 10);

            // back to area between rooms
            planner.AddCourse(betweenRooms, 10);

            // rotate in hallway
            planner.AddRotation(90, 10);

            // move towards first toilet
            var firstToilet = new Engine.Point(975, 475);
            planner.AddCourse(firstToilet, 5);

            // scan both places
            planner.AddRotation(360, 15);

            // move towards second toilet
            var secondToilet = new Engine.Point(1125, 475);
            planner.AddCourse(secondToilet, 5);

            // scan both places
            planner.AddRotation(-270, 15);

            // enter second toilet
            var secondToiletInside = new Engine.Point(1125, 775);
            planner.AddCourse(secondToiletInside, 5);

            // rotate out of toilet
            planner.AddRotation(180, 15);

            // move to entrance of second toilet
            planner.AddCourse(secondToilet, 5);

            // rotate towards dinning area
            planner.AddRotation(90, 15);

            // enter dining area
            var diningArea = new Engine.Point(1275, 475);
            planner.AddCourse(diningArea, 5);

            // scan dining area
            planner.AddRotation(-90, 15);
            planner.AddRotation(180, 15);

            // enter living room
            var livingRoom = new Engine.Point(1275, 700);
            planner.AddCourse(livingRoom, 5);

            // scan living room
            planner.AddRotation(-90, 15);

            return planner;
        }
    }
}
