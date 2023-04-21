using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.BusinessLogic.Containers
{
    public class CommunityContainer : ICommunityContainer
    {

        private readonly ICommunityDataAccess _communityDataAccess;
        private readonly ICommunityMembersDataAccess _communityMembersAccess;
        private readonly ICommunityRulesDataAccess _communityRulesAccess;
        private readonly IPostDataAccess _postDataAccess;



        public CommunityContainer(ICommunityDataAccess communityDataAcess, ICommunityMembersDataAccess communityMembersDataAccess, ICommunityRulesDataAccess communityRulesDataAccess, IPostDataAccess postDataAccess)
        {
            _communityDataAccess = communityDataAcess;
            _communityMembersAccess = communityMembersDataAccess;
            _communityRulesAccess = communityRulesDataAccess;
            _postDataAccess = postDataAccess;


        }

        public List<CommunityDto> LoadCompleteCommunityDtos()
        {
            var communities = _communityDataAccess.LoadCommunitys();

           
            List<CommunityDto> communityDtos = new List<CommunityDto>();

            foreach (var community in communities)
            {
                var rules = _communityRulesAccess.LoadRules(community.CommunityId);
                var UserIds = _communityMembersAccess.LoadUserIds(community.CommunityId);
                var postids = _postDataAccess.GetPostIds(community.CommunityId); 

                CommunityDto communityDto = new CommunityDto();

                
                communityDto.UserId = community.UserId;
                communityDto.Rules = rules;
                communityDto.DateCreated = community.DateCreated;
                communityDto.CommunityId = community.CommunityId;
                communityDto.CommunityName = community.CommunityName;
                communityDto.Description = community.Description;
                communityDto.PostIds = postids;
                communityDto.FollowingUserIds = UserIds;

                communityDtos.Add(communityDto);
            }

            return communityDtos;
        }
    }
}
