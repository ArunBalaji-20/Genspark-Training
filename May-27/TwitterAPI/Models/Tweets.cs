using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterAPI.Models;

public class Tweets
{
    //     Tweets
    // TweetId -pk | postedby -fK(UserId) |content | PostedAt 

    [Key]
    public required string TweetId { get; set; }

    public required string PostedBy { get; set; }

    [ForeignKey("PostedBy")]
    public Users? User { get; set; }
    public required string Content { get; set; }
        
    public DateTime PostedAt { get; set; }    

}

