using PinkWorld.Common.Enums;
using System;

namespace PinkWorld.Common.Responses
{
    public class UserResponse
    {

        public string Id { get; set; }

        public string Document { get; set; }


        public string FirstName { get; set; }


        public string SecondName { get; set; }


        public string LastName { get; set; }


        public string Address { get; set; }


        public string Email { get; set; }


        public Guid ImageId { get; set; }

        public string ImageFacebook { get; set; }

        public LoginType LoginType { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (LoginType == LoginType.Facebook && string.IsNullOrEmpty(ImageFacebook) ||
                    LoginType == LoginType.PinkWorld && ImageId == Guid.Empty)
                {
                    return $"https://localhost:44357/images/no-image.png";
                }

                if (LoginType == LoginType.Facebook)
                {
                    return ImageFacebook;
                }

                return $"https://pinkworld.blob.core.windows.net/users/{ImageId}";
            }
        }



    
        public string PhoneNumber { get; set; }


        public UserType UserType { get; set; }

        public CityResponse City { get; set; }


        public string FullName => $"{FirstName} {LastName}";


        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";
    }
}
