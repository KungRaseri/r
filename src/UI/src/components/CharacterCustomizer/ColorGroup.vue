<template>
    <v-expansion-panel>
        <v-expansion-panel-header>
            {{ title }}
        </v-expansion-panel-header>
        <v-expansion-panel-content>
            <v-container>
                <v-row v-for="i in 8" :key="'r' + i" justify="center" no-gutters>
                    <v-col v-for="j in 8" :key="'c' + j + ':' + i">
                        <ColorSwatch :index="((i - 1) * 8 + j) - 1" :colors="ColorGroup()" @changecolor="ChangeColor" />
                    </v-col>
                </v-row>
            </v-container>
        </v-expansion-panel-content>
    </v-expansion-panel>
</template>

<script lang="ts">
    import { Component, Vue, Prop } from 'vue-property-decorator';
    import ColorSwatch from './ColorSwatch.vue';

    @Component({
        components: {
            ColorSwatch
        },
    })
    export default class ColorGroup extends Vue {
        @Prop(String) name = this.Name;
        @Prop(String) title = this.Title;
        @Prop(String) type = this.Type;
        @Prop(String) target = this.Target;

        public hairSwatches: any[] = [
            '#1C1F21', '#272A2C', '#312E2C', '#35261C', '#4B321F', '#5C3B24', '#6D4C35', '#6B503B', '#765C45', '#7F684E', '#99815D', '#A79369', '#AF9C70', '#BBA063', '#D6B97B', '#DAC38E', '#9F7F59', '#845039', '#682B1F', '#61120C', '#640F0A', '#7C140F', '#A02E19', '#B64B28', '#A2502F', '#AA4E2B', '#626262', '#808080', '#AAAAAA', '#C5C5C5', '#463955', '#5A3F6B', '#763C76', '#ED74E3', '#EB4B93', '#F299BC', '#04959E', '#025F86', '#023974', '#3FA16A', '#217C61', '#185C55', '#B6C034', '#70A90B', '#439D13', '#DCB857', '#E5B103', '#E69102', '#F28831', '#FB8057', '#E28B58', '#D1593C', '#CE3120', '#AD0903', '#880302', '#1F1814', '#291F19', '#2E221B', '#37291E', '#2E2218', '#231B15', '#020202', '#706C66', '#9D7A50'
        ];

        public overlaySwatches: any[] = [
            '#992532', '#C8395D', '#BD516C', '#B8637A', '#A6526B', '#B1434C', '#7F3133', '#A4645D', '#C18779', '#CBA096', '#C6918F', '#AB6F63', '#B06050', '#A84C33', '#B47178', '#CA7F92', '#ED9CBE', '#E775A4', '#DE3E81', '#B34C6E', '#712739', '#4F1F2A', '#AA222F', '#DE2034', '#CF0813', '#E55470', '#DC3FB5', '#C227B2', '#A01CA9', '#6E1875', '#731465', '#56165C', '#6D1A9D', '#1B3771', '#1D4EA7', '#1E74BB', '#21A3CE', '#25C2D2', '#23CCA5', '#27C07D', '#1B9C32', '#148604', '#70D041', '#C5EA34', '#E1E32F', '#FFDD26', '#FAC026', '#F78A27', '#FE5910', '#BE6E19', '#F7C97F', '#FBE5C0', '#F5F5F5', '#B3B4B3', '#919191', '#564E4E', '#180E0E', '#58969E', '#4D6F8C', '#1A2B55', '#A07E6B', '#826355', '#6D5346', '#3E2D27'
        ];

        $axios: any;

        ColorGroup() {
            if (this.Name === "Hair" || this.Name === "FacialHair" || this.Name === "Eyebrows" || this.Name === "ChestHair") {
                return this.hairSwatches;
            }

            return this.overlaySwatches;
        }

        ChangeColor(value: number) {
            let name = this.Name
            let type = this.Type;
            let index = value;
            let target = this.target;

            this.$axios
                .post(
                    "http://framework/SET_COMPONENT_COLOR",
                    {
                        name,
                        type,
                        index,
                        target
                    }
                )
                .catch((error: any) => {
                    console.log("error", error);
                });
        }

        get Name() {
            return this.name;
        }

        set Name(value: string) {
            this.name = value;
        }

        get Title() {
            return this.title;
        }

        set Title(value: string) {
            this.title = value;
        }

        get Type() {
            return this.type;
        }

        set Type(value: string) {
            this.type = value;
        }

        get Target() {
            return this.target;
        }

        set Target(value: string) {
            this.target = value;
        }
    }
</script>

<style scoped>
</style>