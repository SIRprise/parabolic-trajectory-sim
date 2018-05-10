﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ThrowSimulation.Loop
{
    abstract class MainLoop
    {
        protected RenderWindow window;
        protected Color background_color;
        protected uint width, height;
        protected const double target_fps = 60;
        protected const double dt = 1 / target_fps;
        protected Adapter adapter = new Adapter();

        public MainLoop(uint width, uint height, string title)
        {
            this.width = width;
            this.height = height;
            window = new RenderWindow(new VideoMode(this.width, this.height), title, Styles.Titlebar, new ContextSettings { DepthBits = 8, AntialiasingLevel = 2 });
            this.background_color = Color.Black;

            window.KeyPressed += Window_KeyPressed;
            window.KeyReleased += Window_KeyReleased;
            window.MouseButtonPressed += Window_MouseButtonPressed;
            window.MouseButtonReleased += Window_MouseButtonReleased;
            window.MouseMoved += Window_MouseMoved;
        }

        protected abstract void LoadContent();
        protected abstract void Initialize();
        protected abstract void Update(double dt);
        protected abstract void Render(double leftover_time);

        private void Window_MouseMoved(object sender, MouseMoveEventArgs e)
        {
            adapter.cursor.x = e.X;
            adapter.cursor.y = e.Y;
        }

        private void Window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                adapter.LMP_click = true;
            }
        }

        private void Window_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                adapter.LMP_click = false;
            }
        }

        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.Q:
                    adapter.key = 'q';
                    break;
                case Keyboard.Key.A:
                    adapter.key = 'a';
                    break;
                case Keyboard.Key.W:
                    adapter.key = 'w';
                    break;
                case Keyboard.Key.S:
                    adapter.key = 's';
                    break;
                case Keyboard.Key.E:
                    adapter.key = 'e';
                    break;
                case Keyboard.Key.D:
                    adapter.key = 'd';
                    break;
                case Keyboard.Key.R:
                    adapter.key = 'r';
                    break;
                case Keyboard.Key.F:
                    adapter.key = 'f';
                    break;
                case Keyboard.Key.T:
                    adapter.key = 't';
                    break;
                case Keyboard.Key.G:
                    adapter.key = 'g';
                    break;
                case Keyboard.Key.Y:
                    adapter.key = 'y';
                    break;
                case Keyboard.Key.H:
                    adapter.key = 'h';
                    break;
                case Keyboard.Key.F1:
                    adapter.vectors *= -1;
                    break;
                case Keyboard.Key.F2:
                    adapter.fill *= -1;
                    break;
                case Keyboard.Key.C:
                    adapter.clear = true;
                    break;
                case Keyboard.Key.Escape:
                    window.Close();
                    break;
                default:
                    break;
            }
        }

        private void Window_KeyReleased(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.Q:
                    adapter.key = 'x';
                    break;
                case Keyboard.Key.A:
                    adapter.key = 'x';
                    break;
                case Keyboard.Key.W:
                    adapter.key = 'x';
                    break;
                case Keyboard.Key.S:
                    adapter.key = 'x';
                    break;
                case Keyboard.Key.E:
                    adapter.key = 'x';
                    break;
                case Keyboard.Key.D:
                    adapter.key = 'x';
                    break;
                case Keyboard.Key.R:
                    adapter.key = 'x';
                    break;
                case Keyboard.Key.F:
                    adapter.key = 'x';
                    break;
                case Keyboard.Key.T:
                    adapter.key = 'x';
                    break;
                case Keyboard.Key.G:
                    adapter.key = 'x';
                    break;
                case Keyboard.Key.Y:
                    adapter.key = 'x';
                    break;
                case Keyboard.Key.H:
                    adapter.key = 'x';
                    break;
                case Keyboard.Key.C:
                    adapter.clear = false;
                    break;
                default:
                    break;
            }
        }

        public void RUN()
        {
            LoadContent();
            Initialize();

            Clock clock = new Clock();
            double previous_time = clock.ElapsedTime.AsSeconds();
            double accumulator = 0.0;

            while (window.IsOpen)
            {
                double current_time = clock.ElapsedTime.AsSeconds();
                double elapsed_time = current_time - previous_time;
                previous_time = current_time;
                accumulator += elapsed_time;

                window.Clear(background_color);
                window.DispatchEvents();

                if (accumulator > 0.25)
                {
                    accumulator = 0.25;
                }

                while (accumulator >= dt)
                {
                    Update(dt);
                    accumulator -= dt;
                }

                Render(accumulator / dt);
                window.Display();

                double sleeping_time = current_time + dt - clock.ElapsedTime.AsSeconds();
                if (sleeping_time > 0)
                {
                    System.Threading.Thread.Sleep((int)(sleeping_time * 100));
                }
            }
        }
    }
}