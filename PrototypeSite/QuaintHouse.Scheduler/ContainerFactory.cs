using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Ioc;

namespace QuaintHouse.Scheduler
{
    public class ContainerFactory
    {
        private static Container container;

        static ContainerFactory()
        {
            container = new Container();
        }

        public static Container GetContainer()
        {
            return container;
        }

        public static Container GetChildContainer(string name)
        {
            return container.CreateChildContainer(name);
        }
    }
}
