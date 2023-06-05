using SocialMedia.BusinessLogic.Custom_exception;
using SocialMedia.BusinessLogic.Dto;
using SocialMedia.BusinessLogic.Interfaces.IContainer;
using SocialMedia.BusinessLogic.Interfaces.IDataAccess;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SocialMedia.BusinessLogic.Containers
{
    public class CommunityContainer : ICommunityContainer
    {

        private readonly ICommunityDataAccess _communityDataAccess;
        private readonly ICommunityMembersDataAccess _communityMembersAccess;
        private readonly ICommunityRulesDataAccess _communityRulesAccess;
        private readonly IPostDataAccess _postDataAccess;
        private readonly IUserDataAccess _userDataAccess;
        private readonly ICommunityModeratorsDataAccess _communityModeratorsAccess;



        public CommunityContainer(ICommunityDataAccess communityDataAcess, ICommunityMembersDataAccess communityMembersDataAccess, ICommunityRulesDataAccess communityRulesDataAccess, IPostDataAccess postDataAccess, IUserDataAccess userDataAccess, ICommunityModeratorsDataAccess communityModeratorsDataAccess)
        {
            _communityDataAccess = communityDataAcess;
            _communityMembersAccess = communityMembersDataAccess;
            _communityRulesAccess = communityRulesDataAccess;
            _postDataAccess = postDataAccess;
            _userDataAccess = userDataAccess;
            _communityModeratorsAccess = communityModeratorsDataAccess;
        }

        // to be modified
        public void CreateAndSaveCommunity(Guid userId, string communityName, string description)
        {
            var doesUserIdExist = _userDataAccess.DoesUserIdExist(userId);

            var isUserPremium = _userDataAccess.IsUserPremium(userId);
            var isCommunityNameUnique = IsCommunityNameUnique(communityName);

            if(doesUserIdExist == true && isUserPremium == true)
            {
                if(!string.IsNullOrEmpty(communityName) && !string.IsNullOrEmpty(description) && communityName.Length <= 35 && description.Length <= 150)
                {
                    Community community = new Community(userId, communityName, description);
                    _communityDataAccess.SaveCommunity();
                }
                else
                {
                    throw new InvalidInputException();
                }
            }
            else
            {
                throw new AccessException();
            }
            
        }

        public bool IsCommunityNameUnique(string communityName)
        {
            bool isCommunityNameUnique = true;

            foreach (string _ in _communityDataAccess.GetCommunityNames())
            {
                if (communityName == _)
                {
                    isCommunityNameUnique = false;
                    break;
                }

            }
            return isCommunityNameUnique;
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

            var doesCommunityNameExist = _communityDataAccess.DoesCommunityNameExist(communityName);

            if (doesCommunityNameExist == true) 
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
            else
            {
                throw new ItemNotFoundException("Invalid community name");
            }
			
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
            var doesCommunityIdExist = _communityDataAccess.DoesCommunityIdExist(communityId);
            var doesUserIdExist = _userDataAccess.DoesUserIdExist(userId);


            if(doesCommunityIdExist == true && doesUserIdExist == true)
            {
                var doesUserAlreadyFollowCommunity = _communityMembersAccess.CheckRecordExists(communityId, userId);

                if (doesUserAlreadyFollowCommunity == false)
                {
                    _communityMembersAccess.CreateMember(communityId, userId);
                }
                else
                {
                    throw new AccessException("User already follows the community");
                }
               
            }
            else
            {
                throw new ItemNotFoundException();
            }
            
        }

        public void UnfollowCommunity(Guid communityId, Guid userId)
        {
            var doesCommunityIdExist = _communityDataAccess.DoesCommunityIdExist(communityId);
            var doesUserIdExist = _userDataAccess.DoesUserIdExist(userId);

            if(doesCommunityIdExist == true && doesUserIdExist == true)
            {
                var doesUserAlreadyFollowCommunity = _communityMembersAccess.CheckRecordExists(communityId, userId);

                if(doesUserAlreadyFollowCommunity == true)
                {
                    _communityMembersAccess.DeleteMember(communityId, userId);
                }
                else
                {
                    throw new AccessException("User does not follow the community, hence cannot unfollow");
                }
            }
            else
            {
                throw new ItemNotFoundException();
            }

          
        }
    }
}
