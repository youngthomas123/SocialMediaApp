using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.PostRendering
{
	public class TextContent : PostContent
	{
		public string Body { get; set; }


		public override string RenderContent()
		{
			string htmlcode =  $"<div class = 'card-text'> " +
							   $"<p> {Body}</p> " +
							   $"</div>";
				
			return htmlcode;
		}
	}
}
