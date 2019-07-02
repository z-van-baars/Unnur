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
        protected List<Entity> entities = new List<Entity>();
        protected List<Entity> collideableEntities = new List<Entity>();
        protected List<Entity> movingEntities = new List<Entity>();
        protected List<Entity> characters = new List<Entity>();
        public Scene(Point dimensions)
        {
            this.dimensions = dimensions;
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
        public void AddCharacter(Entity newCharacter)
        {
            entities.Add(newCharacter);
            movingEntities.Add(newCharacter);
            collideableEntities.Add(newCharacter);
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
    }
}
