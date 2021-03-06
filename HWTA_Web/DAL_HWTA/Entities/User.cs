﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL_HWTA.Entities
{
    public class User
    {
        public long Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }
        
        public Role Role { get; set; }

        public string ContentType { get; set; }

        public byte[] ProfilePicture { get; set; }


        [InverseProperty(nameof(UserGoal.User))]
        public List<UserGoal> UserGoals { get; set; }

        [InverseProperty(nameof(Friend.User))]
        public List<Friend> Users { get; set; }

        [InverseProperty(nameof(Friend.UserFriend))]
        public List<Friend> UserFriends { get; set; }

    }
}
