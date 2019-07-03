using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnur
{
    class Scene
    {
        protected Point dimensions;
        protected Tile[,] tiles;
        protected List<Entity> entities = new List<Entity>();
        protected List<Entity> collideableEntities = new List<Entity>();
        protected List<Entity> movingEntities = new List<Entity>();
        protected List<Entity> physicalEntities = new List<Entity>();
        protected List<Entity> characters = new List<Entity>();
        public Scene(Point dimensions)
        {
            this.dimensions = new Point(dimensions.X * 32, dimensions.Y * 32);
            tiles = new Tile[dimensions.Y, dimensions.X];
            for (int y = 0; y < dimensions.Y; y++)
            {
                for (int x = 0; x < dimensions.X; x++)
                {
                    tiles[y, x] = new Tile(new Point(x, y));
                }
            }
        }
        public List<Entity> GetEntities()
        {
            return entities;
        }
        public List<Entity> GetCollideableEntities()
        {
            return collideableEntities;
        }
        public List<Entity> GetMovingEntities()
        {
            return movingEntities;
        }
        public List<Entity> GetCharacters()
        {
            return characters;
        }
        public List<Entity> GetPhysicalEntities()
        {
            return physicalEntities;
        }

        public void AddCharacter(Entity newCharacter)
        {
            entities.Add(newCharacter);
            movingEntities.Add(newCharacter);
            collideableEntities.Add(newCharacter);
            physicalEntities.Add(newCharacter);
            characters.Add(newCharacter);
        }
        public void AddMovingEntity(Entity newMovingEntity)
        {
            entities.Add(newMovingEntity);
            movingEntities.Add(newMovingEntity);
            if (newMovingEntity.IsCollideable())
            {
                collideableEntities.Add(newMovingEntity);
            }
        }
        public void AddStaticCollideable(Entity newStaticCollideable)
        {
            entities.Add(newStaticCollideable);
            collideableEntities.Add(newStaticCollideable);
        }
        public void AddPhysicalEntity(Entity newPhysicalEntity)
        {
            entities.Add(newPhysicalEntity);
            collideableEntities.Add(newPhysicalEntity);
            physicalEntities.Add(newPhysicalEntity);
        }
    }
}
