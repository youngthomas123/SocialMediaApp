﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Dto
{
    public class CommentPageDto
    {

        public CommentPageDto() { }



        public DateTime DateCreated { get;  set; }

        public Guid CommentId { get;  set; }

        public string Author { get; set; }

        public string Body { get; set; }

        public int Score { get;  set; }

        public Guid PostId { get;  set; }

		public bool IsUpvoted { get; set; }

		public bool IsDownvoted { get; set; }

        public bool IsReported { get; set; }

	}
}
