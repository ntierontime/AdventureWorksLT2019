import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import { getLocales, findBestAvailableLanguage } from 'react-native-localize';

import en from './translations/en.json';
import es from './translations/es.json';
import fr from './translations/fr.json';
const translations = { en: {translation: en}, es: {translation: es}, fr: {translation: fr} };

const { languageTag } = findBestAvailableLanguage(
    Object.keys(translations),
  ) || { languageTag: 'en' };

i18n.use(initReactI18next).init({
    lng: getLocales()[0].languageCode,
    fallbackLng: 'en',
    resources: translations,
});

export default i18n;