<template>
    <v-expansion-panel v-if="Show">
        <v-expansion-panel-header>
            {{ Name }}
        </v-expansion-panel-header>
        <v-expansion-panel-content>
            <v-container class="panel">
                <v-row dense>
                    <v-col>
                        <v-btn height="100%" block>
                            <v-icon>mdi-arrow-left-thick</v-icon>
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-text-field class="centered-input"/>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            <v-icon>mdi-arrow-right-thick</v-icon>
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            <v-icon>mdi-arrow-left-thick</v-icon>
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-text-field class="centered-input" />
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            <v-icon>mdi-arrow-right-thick</v-icon>
                        </v-btn>
                    </v-col>
                </v-row>
                <v-row v-if="Slider" class="mt-6" dense>
                    <v-slider/>
                </v-row>
            </v-container>
        </v-expansion-panel-content>
    </v-expansion-panel>
</template>

<script lang="ts">
    import { Component, Vue, Prop } from 'vue-property-decorator';

    @Component({
        components: {
        },
    })
    export default class StyleComponent extends Vue {
        @Prop(String) name = this.Name;
        slider = false;
        max = 0;
        show = false;

        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "PED_COMPONENT_DATA":
                        if (this.Name === e.data.pedComp.item) {
                            this.Max = e.data.pedComp.comp.Count;
                            this.Show = e.data.pedComp.comp.HasAnyVariations;
                        }
                        break;
                    default:
                        break;
                }
            });
        }

        get Name() {
            return this.name;
        }

        set Name(value: string) {
            this.name = value;
        }

        get Slider() {
            return this.slider;
        }

        set Slider(value: boolean) {
            this.slider = value;
        }

        get Max() {
            return this.max;
        }

        set Max(value: number) {
            this.max = value;
        }

        get Show() {
            return this.show;
        }

        set Show(value: boolean) {
            this.show = value;
        }
    }
</script>

<style>
</style>

<style scoped>
    .v-btn {
        min-width: 0;
        padding: 0 !important;
    }

    .panel {
        padding: 0;
    }

    .centered-input >>> input {
        text-align: center;
    }

    .centered {
        text-align: center;
    }
</style>