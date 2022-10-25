using System;
using System.Collections.Generic;

namespace SignE.Core.ECS
{
    public class GameSystem
    {
        private List<ComponentPairs> _componentPairs = new List<ComponentPairs>();

        public void Execute()
        {
            
        }

        public void GetComponentPairs(World world)
        {
            Console.WriteLine(GetType().ToString());
            Attribute[] attrs = Attribute.GetCustomAttributes(GetType());


            List<Type> componentTypes = new List<Type>();
            foreach (Attribute attr in attrs)  
            {  
                if (attr is WithAttribute<IComponent>)
                {
                    Type componentType = ((WithAttribute<IComponent>) attr).GetComponentType();
                    componentTypes.Add(componentType);
                }
            }
            this._componentPairs = world.GetComponentPairs(componentTypes);
        }
    }

    public class ComponentPairs
    {
        public List<IComponent> _components = new List<IComponent>();
    }

    public class WithAttribute<T> : Attribute
    {
        public Type GetComponentType()
        {
            return typeof(T);
        }
    }
}