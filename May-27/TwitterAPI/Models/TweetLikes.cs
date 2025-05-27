using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterAPI.Models;

public class TweetLikes
{
    //     TweetLikes
    // ID -pk | TweetId -fk | LikedBy -fk(userId) 

    [Key]
    public int Id { get; set; }

    public required string TweetId { get; set; }
    [ForeignKey("TweetId")]
    public Tweets? Tweet { get; set; }

    public required string LikedBy { get; set; }
    [ForeignKey("LikedBy")]
    public Users? User { get; set; }
}
