﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using ThrowSimulation.BasicElements;
using ThrowSimulation.Objects;

namespace ThrowSimulation.Loop
{
    /// <summary>
    /// Class containing methods to draw all components
    /// </summary>
    class Drawer
    {
        /// <summary>
        /// Draws cannon
        /// </summary>
        /// <param name="window"></param>
        /// <param name="cannon"></param>
        public void DrawCanon(RenderWindow window, Cannon cannon)
        {
            VertexArray ver_arr = new VertexArray(PrimitiveType.Quads);
            Color color = new Color(255, 100, 100);

            for (int i = 0; i < cannon.shape.Length; i++)
            {
                ver_arr.Append(new Vertex(new Vector2f((float)cannon.shape[i].x, (float)cannon.shape[i].y), color));
            }
            ver_arr.Append(new Vertex(new Vector2f((float)cannon.shape[0].x, (float)cannon.shape[0].y), color));

            window.Draw(ver_arr);
        }

        /// <summary>
        /// Draws single projectile
        /// </summary>
        /// <param name="window"></param>
        /// <param name="projectile"></param>
        /// <param name="fill"></param>
        public void DrawProjectile(RenderWindow window, Projectile projectile, int fill)
        {
            Circle circle = new Circle(projectile.hitch, projectile.radius, 15);
            VertexArray ver_arr = new VertexArray(PrimitiveType.TrianglesFan);
            if (fill == -1)
            {
                ver_arr = new VertexArray(PrimitiveType.LinesStrip);
            }

            for (int i = 0; i < circle.vertices.Length; i++)
            {
                ver_arr.Append(new Vertex(new Vector2f((float)circle.vertices[i].x, (float)circle.vertices[i].y), new Color(projectile.color[0], projectile.color[1], projectile.color[2])));
            }

            ver_arr.Append(new Vertex(new Vector2f((float)circle.vertices[0].x, (float)circle.vertices[0].y), new Color(projectile.color[0], projectile.color[1], projectile.color[2])));
            window.Draw(ver_arr);
        }

        /// <summary>
        /// Draws fields of projectiles vectors
        /// </summary>
        /// <param name="window"></param>
        /// <param name="projectile"></param>
        public void DrawVectorsField(RenderWindow window, Projectile projectile)
        {
            VertexArray ver_arr = new VertexArray(PrimitiveType.Lines);

            for (int i = 0; i < projectile.vectors.const_forces.Length; i++)
            {
                ver_arr.Append(new Vertex(new Vector2f((float)projectile.hitch.x, (float)projectile.hitch.y), Color.Green));
                ver_arr.Append(new Vertex(new Vector2f((float)(projectile.hitch.x + projectile.vectors.const_forces[i].x), (float)(projectile.hitch.y + projectile.vectors.const_forces[i].y)), Color.Green));
            }

            ver_arr.Append(new Vertex(new Vector2f((float)projectile.hitch.x, (float)projectile.hitch.y), Color.Magenta));
            ver_arr.Append(new Vertex(new Vector2f((float)(projectile.hitch.x + projectile.vectors.momentum.x), (float)(projectile.hitch.y + projectile.vectors.momentum.y)), Color.Magenta));

            window.Draw(ver_arr);
        }

        /// <summary>
        /// Draws single text
        /// </summary>
        /// <param name="window"></param>
        /// <param name="font"></param>
        /// <param name="value"></param>
        /// <param name="size"></param>
        /// <param name="position"></param>
        public void DrawText(RenderWindow window, Font font, string value, uint size, Point position)
        {
            Text text = new Text(value, font, size);
            text.Position = new Vector2f((float)(position.x), (float)(position.y));
            text.Color = new Color(50, 255, 130);
            window.Draw(text);
        }

        /// <summary>
        /// Draws parameters of scene
        /// </summary>
        /// <param name="window"></param>
        /// <param name="font"></param>
        /// <param name="scene"></param>
        public void DrawSceneInfo(RenderWindow window, Font font, Scene scene)
        {
            VertexArray ver_arr = new VertexArray(PrimitiveType.Lines);
            Color line_fading_color = new Color(0, 150, 150, 10);
            Color line_color = new Color(0, 230, 230);


            for (int i = 0; i < 8; i++)
            {
                ver_arr.Append(new Vertex(new Vector2f(0, (float)(i * scene.text_height + 10)),line_color ));
                ver_arr.Append(new Vertex(new Vector2f((float)scene.width, (float)(i * scene.text_height + 10)), line_fading_color));
            }
            window.Draw(ver_arr);

            DrawText(window, font, "Gravity: " + scene.gravity.value.ToString(), (uint)scene.gravity.height, scene.gravity.hitch);
            DrawText(window, font, "Environment density: " + scene.environment_density.value.ToString(), (uint)scene.environment_density.height, scene.environment_density.hitch);
            DrawText(window, font, "Shot power: " + scene.shot_power.value.ToString(), (uint)scene.shot_power.height, scene.shot_power.hitch);
            DrawText(window, font, "Projectile radius: " + scene.projectile_radius.value.ToString(), (uint)scene.projectile_radius.height, scene.projectile_radius.hitch);
            DrawText(window, font, "Projectile mass: " + scene.projectile_mass.value.ToString(), (uint)scene.projectile_mass.height, scene.projectile_mass.hitch);
            DrawText(window, font, "Projectile restitution: " + scene.projectile_restitution.value.ToString(), (uint)scene.projectile_restitution.height, scene.projectile_restitution.hitch);
            DrawText(window, font, "Resistance force: " + scene.resistance_force.value.ToString(), (uint)scene.resistance_force.height, scene.resistance_force.hitch);
        }

        /// <summary>
        /// Draws cannon, projectiles and scene info
        /// </summary>
        /// <param name="window"></param>
        /// <param name="scene"></param>
        /// <param name="font"></param>
        /// <param name="vectors"></param>
        /// <param name="fill"></param>
        public void DrawScene(RenderWindow window, Scene scene, Font font, int vectors, int fill)
        {
            for (int i = 0; i < scene.projectiles.Count; i++)
            {
                DrawProjectile(window, scene.projectiles.ElementAt(i), fill);
                if (vectors == -1)
                {
                    DrawVectorsField(window, scene.projectiles.ElementAt(i));
                }
            }
            DrawCanon(window, scene.cannon);
            DrawSceneInfo(window, font, scene);
        }
    }
}
