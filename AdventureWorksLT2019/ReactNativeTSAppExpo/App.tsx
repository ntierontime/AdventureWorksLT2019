import React, { useState, useEffect } from "react";

import {
    StyleSheet,
    ScrollView,
    Text,
    View,
    BackHandler
} from 'react-native';

import { List, Divider, Checkbox, Avatar, Switch } from 'react-native-paper';
import Constants from "expo-constants";

import {
    SafeAreaView,
    SafeAreaProvider,
} from 'react-native-safe-area-context';
import { Provider as PaperProvider } from 'react-native-paper';
import { Provider as StoreProvider } from 'react-redux';
import store from './src/store/Store';

import { EXAMPLE_LIST } from "./src/ExampleList";

import { useTranslation } from 'react-i18next';
import i18n from "./src/i18n";
import ScreenWrapper from "./src/ScreenWrapper";
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
        <StoreProvider store={store}><PaperProvider><SafeAreaProvider>{
            EXAMPLE_LIST[exampleIndex].component
        }
        </SafeAreaProvider></PaperProvider></StoreProvider>
    )

    return (
        <StoreProvider store={store}><PaperProvider><SafeAreaProvider>
            <SafeAreaView>
                <Text style={styles.heading}>
                    {t('AdvancedSearch')} React Native Expo Examples
                </Text>
                <ScreenWrapper>
      <List.Section title="Text-only">
      {EXAMPLE_LIST.map((l, i) => (

        <List.Item key={i}  onPress={() => setExampleIndex(i)}
          title={l.name}
        />
                ))}
      </List.Section>
    </ScreenWrapper>
                {/* <ScrollView>
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
                </ScrollView> */}
            </SafeAreaView>
        </SafeAreaProvider></PaperProvider></StoreProvider>
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
