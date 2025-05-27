using System;
using System.ComponentModel.DataAnnotations;

namespace TwitterAPI.Models;

public class Hashtags
{
    //     Hashtags
    // HashtagId -pk | Hashtag

    [Key]
    public required string HashtagId { get; set; }

    public required string Tag { get; set; }
}
