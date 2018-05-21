using System;
using System.Collections.Generic;
using System.Text;
using MoviesAPI.DbModels;

namespace MoviesAPI.Tests.Builders
{
    public class ActorBuilder
    {
        private int _id;
        private string _firstname;
        private string _lastname;
        private ICollection<MovieRole> _roles;

        public ActorBuilder()
        {
            _id = 1;
            _firstname = "";
            _lastname = "";
            _roles = new List<MovieRole>();
        }

        public ActorBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public ActorBuilder WithFirstname(string firstname)
        {
            _firstname = firstname;
            return this;
        }

        public ActorBuilder WithId(string lastname)
        {
            _lastname = lastname;
            return this;
        }

        public ActorBuilder WithRoles(ICollection<MovieRole> roles)
        {
            _roles = roles;
            return this;
        }

        public Actor Build()
        {
            return new Actor()
            {
                Firstname = _firstname,
                Lastname = _lastname,
                Id = _id,
                Roles = _roles
            };
        }
    }
}
