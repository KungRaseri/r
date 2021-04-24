import Vue from 'vue';
import Vuetify from 'vuetify/lib/framework';
import colors from 'vuetify/lib/util/colors';

Vue.use(Vuetify);

export default new Vuetify({
    theme: {
        dark: true,
        themes: {
            dark: {
                primary: "#0C5273",
                secondary: "#117EAC",
                accent: "#209DD4",
                error: colors.red.darken1,
                info: colors.blue.lighten1,
                success: colors.green.darken1,
                warning: colors.yellow.lighten2
            }
        }
    },
});
