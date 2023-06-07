using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.PostRendering
{
	public class ImageContent : PostContent
	{
		public string Image { get; set; }

		public override string RenderContent()
		{
			string htmlcode = $"<img src= {Image} alt=Image class=img-fluid>";



			return htmlcode;
		}
	}
}
