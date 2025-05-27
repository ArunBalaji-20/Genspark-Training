using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterAPI.Models;

public class Followings
{
    // ID -Pk | UserId(owner) -fk | followerId(userId of follower) -fk

    public int Id { get; set; }

    public required string UserId { get; set; }
    [ForeignKey("UserId")]
    public Users? User { get; set; }

    public required string FollowerId { get; set; }
    [ForeignKey("FollowerId")]
    public Users? follower { get; set; }

}
