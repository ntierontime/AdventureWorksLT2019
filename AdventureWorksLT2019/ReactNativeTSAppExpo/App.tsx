import React, { useState, useEffect } from "react";
import { StatusBar } from 'expo-status-bar';
import {
    StyleSheet,
    SafeAreaView,
    ScrollView,
    View,
    BackHandler
} from 'react-native';
import { Text, ListItem } from "react-native-elements";
import Constants from "expo-constants";

import { Provider } from 'react-redux';
import store from './src/store/Store';

import { EXAMPLE_LIST } from "./src/ExampleList";

import { useTranslation } from 'react-i18next';
import i18n from "./src/i18n";
const initI18n = i18n;

export default function App() {
    const { t, i18n } = useTranslation();
    const [exampleIndex, setExampleIndex] = useState<number | null>(null);

    // Handle when user press Hardware Back Button
    useEffect(() => {
        const backAction = () => {
            // Go back to Example List
            if (exampleIndex !== null) {
                setExampleIndex(null);
            }
            // Exit app if user currently in Example List
            else {
                BackHandler.exitApp();
            }

            return true;
        };

        // https://reactnative.dev/docs/backhandler
        const backHandler = BackHandler.addEventListener(
            "hardwareBackPress",
            backAction
        );

        return () => backHandler.remove();
    }, [exampleIndex]);

    if (exampleIndex !== null) return (
        <Provider store={store}> {
            EXAMPLE_LIST[exampleIndex].component
        }
        </Provider>
    )

    return (
        <Provider store={store}>
            <SafeAreaView style={styles.container}>
                <Text h4 style={styles.heading}>
                    {t('AdvancedSearch')} React Native Expo Examples
                </Text>

                <ScrollView>
                    {EXAMPLE_LIST.map((l, i) => (
                        <ListItem key={i} bottomDivider onPress={() => setExampleIndex(i)}>
                            <View>
                                <Text>Level {l.level}</Text>
                            </View>

                            <ListItem.Content>
                                <ListItem.Title style={styles.title}>{l.name}</ListItem.Title>
                            </ListItem.Content>
                        </ListItem>
                    ))}
                </ScrollView>
            </SafeAreaView>
        </Provider>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        paddingTop: Constants.statusBarHeight,
    },
    heading: {
        textAlign: "center",
        padding: 12,
    },
    title: {
        fontWeight: "bold",
    },
});
