export enum AppWellKnownActions {
    // 1. BooleanStatus Buttons
    /// <summary>
    /// Flag on IItem, Entity, EntityAlbumItem
    /// </summary>
    LikeAndUnLike = "LikeAndUnLike",            // InUse: BooleanStatusButton
    Like = "Like",                              // InUse: BooleanStatusButton
    UnLike = "UnLike",                          // InUse: BooleanStatusButton
    ReportAbuse = "ReportAbuse",                // InUse: BooleanStatusButton
    
    /// <summary>
    /// Action on Item, Entity, 
    /// </summary>
    Bookmark = "Bookmark",                      // InUse: BooleanStatusButton
    Favorite = "Favorite",                      // InUse: BooleanStatusButton
    Recommend = "Recommend",                    // InUse: BooleanStatusButton
    Watch = "Watch",                            // InUse: BooleanStatusButton

    /// <summary>
    /// Flag on Entity
    /// </summary>
    Connect = "Connect",                        // InUse: BooleanStatusButton
    Follow = "Follow",                          // InUse: BooleanStatusButton
    Mute = "Mute",                              // InUse: BooleanStatusButton
    Block = "Block",                            // InUse: BooleanStatusButton

    // 2. SocialShare
    /// <summary>
    /// Action on Item, Entity, EntityAlbumItem
    /// </summary>
    SocialShare = "SocialShare",                // InUse: ShareShareButton

    // 3. Workflow next steps
    /// <summary>
    /// Action on Workflow, please see generted Enums.ts/EnumCnsumerWorkflowStates.ts
    /// </summary>
    Cancel = "Cancel",                          // InUse: EnumCnsumerWorkflowStates
    Revoke = "Revoke",                          // InUse: EnumCnsumerWorkflowStates

    // 4. Workflow init step
    // Workflow Init Step
    Register = "Register",
    /// <summary>
    /// Action on Item with Scheduling - ServiceBooking, Class,
    /// </summary>
    BookNow = "BookNow",
    /// <summary>
    /// Action on Class
    /// </summary>
    Join = "Join",
    /// <summary>
    /// Action on Entity -> EntityGroup
    /// </summary>
    JoinGroup = "JoinGroup",
    /// <summary>
    /// Action on Entity -> EntityGroup
    /// </summary>
    Checkout = "Checkout",

    // 100. the rest
    Subscribe = "Subscribe",

    /// <summary>
    /// Action on Entity
    /// </summary>
    Message = "Message",
    Call = "Call",

    /// <summary>
    /// Action on Item
    /// </summary>
    ShoppingCart = "ShoppingCart",
    AddToCart = "AddToCart",
    RemoveFromShoppngCart = "RemoveFromShoppngCart",
    
    /// <summary>
    /// Action on Item, Entity, 
    /// </summary>
    Rate = "Rate",
    WriteAReview = "WriteAReview",
    Archieve = "Archieve",


    /// <summary>
    /// Action on Item, Entity, EntityAlbumItem
    /// </summary>
    Download = "Download",
    Repost = "Repost",

    /// <summary>
    /// Action on Entity -> Entity
    /// </summary>
    Donate = "Donate",

    /// <summary>
    /// ?
    /// </summary>
    ApplyJob = "ApplyJob",
}
