import { AppWellKnownActions } from "./AppWellKnownActions";
import { ButtonTypes } from "./buttonGroups/ButtonTypes";

export interface ButtonSetting {
    action: AppWellKnownActions;
    buttonType: ButtonTypes;
}

export interface ConsumerOnPartialViewProps<TIdentifier, TConsumerOn, TConsumerOnSummary> {
    targetIdentifier: TIdentifier,
    data?: any,
    consumerOn: TConsumerOn,
    consumerOnSummary?: TConsumerOnSummary,
    actionsVisible?: ButtonSetting[],
    actionsDropDown?: ButtonSetting[],
}


export interface ConsumerOnWithOwnerPartialViewProps<TIdentifier, TConsumerOn, TConsumerOnSummary, TOwnerIdentifier, TOwnerConsumerOn, TOwnerConsumerOnSummary> extends ConsumerOnPartialViewProps<TIdentifier, TConsumerOn, TConsumerOnSummary>{
    toLoadOwner?: boolean;
    ownerIdentifier: TOwnerIdentifier,
    ownerConsumerOn: TOwnerConsumerOn,
    ownerConsumerOnSummary?: TOwnerConsumerOnSummary,
    // ownerActionsVisible?: ButtonSetting[],
    // ownerActionsDropDown?: ButtonSetting[],
}
