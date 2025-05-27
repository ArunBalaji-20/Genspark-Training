using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterAPI.Models;

public class FilterByHashtag
{
    //     FilterByHashtag
    // FilterId-pk | TweetId-fk | HashtagId-fk

    [Key]
    public int Id { get; set; }

    public required string TweetId { get; set; }
    [ForeignKey("TweetId")]
    public Tweets? Tweet { get; set; }

    public required string HashtagId { get; set; }
    [ForeignKey("HashtagId")]
    public Hashtags? Hashtag{ get; set; }
}
