public record FollowerResponse(
    Guid followingUserId,
    Guid followedUserId
);