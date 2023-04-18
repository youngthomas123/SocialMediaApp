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

        private readonly ICommunityDataAccess _communityDataAcess;
        private readonly ICommunityMembersDataAccess _communityMembersAcess;
        private readonly ICommunityRulesDataAccess _communityRulesAcess;
        private readonly IPostDataAccess _postDataAccess;



        public CommunityContainer(ICommunityDataAccess communityDataAcess, ICommunityMembersDataAccess communityMembersDataAccess, ICommunityRulesDataAccess communityRulesDataAccess, IPostDataAccess postDataAccess)
        {
            _communityDataAcess = communityDataAcess;
            _communityMembersAcess = communityMembersDataAccess;
            _communityRulesAcess = communityRulesDataAccess;
            _postDataAccess = postDataAccess;


        }

        public List<CommunityDto> LoadCompleteCommunityDtos()
        {
            var communities = _communityDataAcess.LoadCommunitys();

           
            List<CommunityDto> communityDtos = new List<CommunityDto>();

            foreach (var community in communities)
            {
                var rules = _communityRulesAcess.LoadRules(community.CommunityId);
                var memberUsernames = _communityMembersAcess.LoadMembers(community.CommunityId);
                var postids = _postDataAccess.GetPostIds(community.CommunityId); 

                CommunityDto communityDto = new CommunityDto();

                communityDto.Creator = community.Creator;
                communityDto.MemberUsernames = memberUsernames;
                communityDto.Rules = rules;
                communityDto.DateCreated = community.DateCreated;
                communityDto.CommunityId = community.CommunityId;
                communityDto.CommunityName = community.Name;
                communityDto.Description = community.Description;
                communityDto.PostId = postids;

                communityDtos.Add(communityDto);
            }

            return communityDtos;
        }
    }
}
