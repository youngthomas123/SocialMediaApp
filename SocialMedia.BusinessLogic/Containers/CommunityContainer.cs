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

        public List<CommunityFullDto> LoadCompleteCommunityDtos()
        {
            var communities = _communityDataAccess.LoadCommunitys();

           
            List<CommunityFullDto> communityFullDtos = new List<CommunityFullDto>();

            foreach (var community in communities)
            {
                var rules = _communityRulesAccess.LoadRules(community.CommunityId);
                var UserIds = _communityMembersAccess.LoadUserIds(community.CommunityId);
                var postids = _postDataAccess.GetPostIds(community.CommunityId); 

                CommunityFullDto communityFullDto = new CommunityFullDto();


                communityFullDto.UserId = community.UserId;
                communityFullDto.Rules = rules;
                communityFullDto.DateCreated = community.DateCreated;
                communityFullDto.CommunityId = community.CommunityId;
                communityFullDto.CommunityName = community.CommunityName;
                communityFullDto.Description = community.Description;
                communityFullDto.PostIds = postids;
                communityFullDto.FollowingUserIds = UserIds;

                communityFullDtos.Add(communityFullDto);
            }

            return communityFullDtos;
        }

		public CommunityFullDto LoadCompleteCommunityDto(string communityName)
		{
			var community = _communityDataAccess.LoadCommunity(communityName);


				var rules = _communityRulesAccess.LoadRules(community.CommunityId);
				var UserIds = _communityMembersAccess.LoadUserIds(community.CommunityId);
				var postids = _postDataAccess.GetPostIds(community.CommunityId);

				CommunityFullDto communityFullDto = new CommunityFullDto();


				communityFullDto.UserId = community.UserId;
				communityFullDto.Rules = rules;
				communityFullDto.DateCreated = community.DateCreated;
				communityFullDto.CommunityId = community.CommunityId;
				communityFullDto.CommunityName = community.CommunityName;
				communityFullDto.Description = community.Description;
				communityFullDto.PostIds = postids;
				communityFullDto.FollowingUserIds = UserIds;

				

			return communityFullDto;
		}

		public List<string> LoadCommunityNames()
        {
            var communityNames = _communityDataAccess.GetCommunityNames();

            return communityNames;
        }
        public List<CommunityIdentityDto> LoadCommunityIdentityDtos()
        {
            var NameAndIdArrayList = _communityDataAccess.GetCommunityNameAndId();

            List <CommunityIdentityDto> communityIdentityDtos = new List<CommunityIdentityDto>();   

            foreach (string[] arr in NameAndIdArrayList)
            {
                CommunityIdentityDto communityIdentityDto = new CommunityIdentityDto();

                communityIdentityDto.CommunityName = arr[0];
                communityIdentityDto.CommunityId = arr[1];

                communityIdentityDtos.Add (communityIdentityDto);
            }

            return communityIdentityDtos;
        }
        public void SaveRawCommunity(Community community)
        {
            _communityDataAccess.SaveCommunity(community);
        }

        public string GetCommunityId(string communityName)
        {
            var id = _communityDataAccess.GetCommunityId(communityName);
            return id;
        }
        public List<string>GetCommunityRules(Guid communityId)
        {
            var rules = _communityRulesAccess.LoadRules(communityId);
            return rules;
        }

        public List<string[]>SearchCommunity(string query)
        {
			var namesAndIds = _communityDataAccess.SearchCommunityAndId(query);


			return namesAndIds;
		}

        public void FollowCommunity(Guid communityId, Guid userId)
        {
            _communityMembersAccess.CreateMember(communityId, userId);
        }

        public void UnfollowCommunity(Guid communityId, Guid userId)
        {
            _communityMembersAccess?.DeleteMember(communityId, userId);
        }
    }
}
