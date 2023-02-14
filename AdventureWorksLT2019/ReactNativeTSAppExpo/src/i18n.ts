import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import { getLocales } from 'expo-localization';

import {
    en,
    es,
    fr,
    registerTranslation,
    // @ts-ignore TODO: try to fix expo to work with local library
  } from 'react-native-paper-dates'

import enJson from './translations/en.json';
import esJson from './translations/es.json';
import frJson from './translations/fr.json';

const translations = { en: {translation: enJson}, es: {translation: esJson}, fr: {translation: frJson} };

i18n.use(initReactI18next).init({
    // the following line is fixing: i18next::pluralResolver: Your environment seems not to be Intl API compatible, use an Intl.PluralRules polyfill. Will fallback to the compatibilityJSON v3 format handling.
    compatibilityJSON: 'v3',
    lng: getLocales()[0].languageCode,
    fallbackLng: 'en',
    resources: translations,
    lowerCaseLng: true,
    debug: false,
    fallbackNS: false,
    interpolation: {
        escapeValue: false, // not needed for react as it escapes by default
    }
});

export default i18n;

if(i18n.language === 'en') {
    registerTranslation(i18n.language, en)
}
else if(i18n.language === 'es') {
    registerTranslation(i18n.language, es)
}
else if(i18n.language === 'fr') {
    registerTranslation(i18n.language, fr)
}
else { // this is fallback
    registerTranslation(i18n.language, en)
}

// https://www.i18next.com/translation-function/formatting
export const i18nFormats = {
    dateTime: {
        format: "{{val, datetime}}",
        dateShort: {
            dateStyle: 'short',
        },
        dateLong: {
            dateStyle: 'long',
        },
        dateFull: {
            dateStyle: 'full',
        },
        dateTimeShort: {
            dateStyle: 'short', timeStyle: 'short',
        },
        dateTimeLong: {
            dateStyle: 'long', timeStyle: 'long',
        },
        dateTimeFull: {
            dateStyle: 'full', timeStyle: 'full',
        },
    },
    number: {
        format: "{{val, number}}",
    },
    double: {
        format: "{{val, number(minimumFractionDigits: 2)}}",
    },
    currency: {
        format: "{{val, currency}}",
    },
};

