using System;
using System.Collections.Generic;
using System.Text;
using MoviesAPI.DbModels;

namespace MoviesAPI.Tests.Builders
{
    public class ActorBuilder
    {
        private int id;
        private string firstname;
        private string lastname;
        private ICollection<MovieRole> roles;

        public ActorBuilder()
        {
            id = 1;
            firstname = "";
            lastname = "";
            roles = new List<MovieRole>();
        }

        public ActorBuilder WithId(int id)
        {
            this.id = id;
            return this;
        }

        public ActorBuilder WithFirstname(string firstname)
        {
            this.firstname = firstname;
            return this;
        }

        public ActorBuilder WithId(string lastname)
        {
            this.lastname = lastname;
            return this;
        }

        public ActorBuilder WithRoles(ICollection<MovieRole> roles)
        {
            this.roles = roles;
            return this;
        }

        public Actor Build()
        {
            return new Actor()
            {
                Firstname = firstname,
                Lastname = lastname,
                Id = id,
                Roles = roles
            };
        }
    }
}
