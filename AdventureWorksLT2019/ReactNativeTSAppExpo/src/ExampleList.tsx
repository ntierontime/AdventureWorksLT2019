import { FlatList } from 'react-native';

import type { StackNavigationProp } from '@react-navigation/stack';
import { Divider, List } from 'react-native-paper';
import { useSafeAreaInsets } from 'react-native-safe-area-context';

import { useExampleTheme } from '.';

const examples = [
    'HelloWorld1',
    'BuildVersionIndexPage',
    'BuildVersionCreatePage',
]

type Props = {
    navigation: StackNavigationProp<{ [key: string]: undefined }>;
};

export default function ExampleList({ navigation }: Props) {
    const keyExtractor = (item: string) => item;
    const { colors, isV3 } = useExampleTheme();
    const safeArea = useSafeAreaInsets();

    const renderItem = ({ item }: { item: string }) => {

        return (
            <List.Item title={item} onPress={() => navigation.navigate(item)} />
        );
    };

    return (
        <FlatList
            contentContainerStyle={{
                backgroundColor: colors.background,
                paddingBottom: safeArea.bottom,
                paddingLeft: safeArea.left,
                paddingRight: safeArea.right,
            }}
            style={{
                backgroundColor: colors.background,
            }}
            showsVerticalScrollIndicator={false}
            ItemSeparatorComponent={Divider}
            renderItem={renderItem}
            keyExtractor={keyExtractor}
            data={examples}
        />
    );
}
