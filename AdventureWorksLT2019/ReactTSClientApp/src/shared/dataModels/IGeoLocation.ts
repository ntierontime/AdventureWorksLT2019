export interface IGeoLocation {
    IPv4: string;
    city: string;
    country_code: string;
    country_name: string;
    latitude: number;
    longitude: number;
    postal: string;
    state: string;
}

export interface IGeoLocationFromNavigator {
    accuracy: number;
    altitude?: number;
    altitudeAccuracy?: number;
    latitude: number;
    longitude: number;
    speed?: string;
}