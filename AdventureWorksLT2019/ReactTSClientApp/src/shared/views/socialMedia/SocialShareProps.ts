import { ButtonTypes } from "../buttonGroups/ButtonTypes";

export interface SocialShareProps {
    buttonType?: ButtonTypes;
    shareUrl: string;
    title: string;
    // used by  Facebook, Hatena, OK, Reddit, Tumblr
    withCount: boolean;
    // used by  Pinterest, VK, OK, Weibo
    exampleImage: string;
  }