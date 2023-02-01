export interface AdvancedSearchPartialViewProps<TAdvancedQuery> {
    advancedQuery: TAdvancedQuery,
    submitAction: (query: TAdvancedQuery) => void,
    doneAction: () => void,
}
