using Microsoft.Extensions.DependencyInjection;
using SocialMedia.BusinessLogic.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class TestClass
    {
        private readonly IServiceProvider _serviceProvider;

        public TestClass(IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider;
        
        }

        public void Run()
        {

            var commentContainer = _serviceProvider.GetService<CommentContainer>();

            var loadedComments = commentContainer.GetComments();

            
        }

    }
}
