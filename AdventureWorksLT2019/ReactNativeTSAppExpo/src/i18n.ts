import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
// import { getLocales, findBestAvailableLanguage } from 'react-native-localize';

import en from './translations/en.json';
import es from './translations/es.json';
import fr from './translations/fr.json';
const translations = { en: {translation: en}, es: {translation: es}, fr: {translation: fr} };

// const { languageTag } = findBestAvailableLanguage(
//     Object.keys(translations),
//   ) || { languageTag: 'en' };

i18n.use(initReactI18next).init({
    // the following line is fixing: i18next::pluralResolver: Your environment seems not to be Intl API compatible, use an Intl.PluralRules polyfill. Will fallback to the compatibilityJSON v3 format handling.
    compatibilityJSON: 'v3',
    lng: 'en', //getLocales()[0].languageCode,
    fallbackLng: 'en',
    resources: translations,
});

export default i18n;