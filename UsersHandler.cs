using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess_drag_test
{
    internal static class UsersHandler
    {
        internal static User[] usersNickNames;

        static UsersHandler()
        {
            usersNickNames = new User[] {
                new User("Rei", Image.FromFile("../../chess figures/Rei.png"), UserSocialTypes.FB),
                new User("Kikyo", Image.FromFile("../../chess figures/Kikyo.png"), UserSocialTypes.Skype),
                new User("Hokuto", Image.FromFile("../../chess figures/Hokuto.png"), UserSocialTypes.FB),
                new User("Asuka", Image.FromFile("../../chess figures/Asuka.png"), UserSocialTypes.FB),
                new User("Saber", Image.FromFile("../../chess figures/Saber.png"), UserSocialTypes.SkypeAway)
            };

            usersNickNames[1].invitationSent = true;
            usersNickNames[2].invitationReceived = true;
            usersNickNames[3].invitationSent = true;
            usersNickNames[3].invitationReceived = true;
        }

        internal static int UsersNumber
        {
            get { return usersNickNames.Length; }
        }

        internal static User getUserAtIndex(int i)
        {
            return usersNickNames[i];
        }
    }

    internal class User
    {
        internal string nickName;
        internal Image photo;
        internal UserSocialTypes type;
        internal bool invitationSent = false;
        internal bool invitationReceived = false;
        internal int invMeToUserExpirationCount = 0;
        internal int invUserToMeExpirationCount = 0;
        internal bool[] invitationISentToUserOptions = new bool[] { false, false, true, true, false, false, false, false };
        internal bool[] invitationUserSentToMeOptions = new bool[] { false, false, true, false, true, false, true, false };
        internal bool markedInForm = false;

        internal User(string nickName, Image photo, UserSocialTypes type)
        {
            this.nickName = nickName;
            this.photo = photo;
            this.type = type;
        }
    }

    public enum UserSocialTypes
    {
        FB,
        Skype,
        SkypeAway
    }

    public enum invOptions
    {
        meWhite,
        meBlack,
        anyColor,
        time15min,
        time30min,
        time45min,
        time60min,
        noTime,
    }
}
