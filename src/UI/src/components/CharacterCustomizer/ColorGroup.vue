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
            "#0f0f0f", "#1c1a1a", "#524738", "#8c4c2a",
            "#ab5124", "#d45c2c", "#e37b52", "#e8ad6d",
            "#ffc687", "#f7cf92", "#fce19a", "#fce8b1",
            "#fff7bf", "#ffee80", "#ffee9c", "#fcefae",
            "#fcd981", "#f5ae62", "#f56138", "#fc3e28",
            "#fc4828", "#fc5d28", "#ff7230", "#ff7f36",
            "#ffa04d", "#ff943d", "#bababa", "#d6d6d6",
            "#ebebeb", "#fcfcfc", "#a838c7", "#c163db",
            "#e657bb", "#ff2bdc", "#ff4dc6", "#ffb8e2",
            "#49fceb", "#3ff2ec", "#209ed4", "#51e868",
            "#58ed98", "#2bccb1", "#b5f53d", "#cafc4c",
            "#68eb38", "#fce75b", "#ffdf29", "#fac228",
            "#fc9235", "#ff752b", "#ff863b", "#ff5c33",
            "#fc4c28", "#e31010", "#a30a0a", "#382319",
            "#57392b", "#663d2a", "#7d4c33", "#5e3825",
            "#4f3629", "#000000", "#fffbd4", "#fffce3",
        ];

        public overlaySwatches: any[] = [
            "#931321", "#c5274e", "#b83f5c", "#b1526a",
            "#9e415c", "#aa313b", "#732022", "#a15950",
            "#bd7e6e", "#c5968a", "#c08683", "#a46253",
            "#ab533f", "#a34127", "#ad646a", "#c9798d",
            "#ed8fb5", "#e86a9f", "#de2c75", "#ad3b60",
            "#631327", "#400e19", "#a5121e", "#e4142c",
            "#d3020b", "#e64262", "#dc2cae", "#c016ac",
            "#960ba1", "#610768", "#640657", "#47054c",
            "#5f0895", "#0c2969", "#0b3ca0", "#0c66b6",
            "#0f99cb", "#12bdce", "#12c89c", "#15bb70",
            "#108f2c", "#0d7302", "#62cd2f", "#bfe921",
            "#dee01c", "#ffda14", "#fabb15", "#f88216",
            "#fe5000", "#9b4c14", "#f7c474", "#fbe2bb",
            "#e9e9e9", "#acacac", "#858685", "#484040",
            "#0c0606", "#4a8f96", "#3e6383", "#0c1f4c",
            "#987560", "#785849", "#5f4539", "#31231d",
        ];

        $axios: any;

        ColorGroup() {
            if (this.Type === "overlays") {
                return this.overlaySwatches;
            }
            
            return this.hairSwatches;
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