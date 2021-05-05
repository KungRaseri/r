<template>
    <v-expansion-panel v-if="Show">
        <v-expansion-panel-header>
            {{ Name }}
        </v-expansion-panel-header>
        <v-expansion-panel-content>
            <v-container class="panel">
                <v-row dense>
                    <v-col>
                        <v-btn height="100%" block @click="ChangeItemIndex(-1)" :disabled="ItemDisable('left')">
                            <v-icon>mdi-arrow-left-thick</v-icon>
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-text-field class="centered-input" v-model="ItemIndex" :disabled="ItemDisable('input')"/>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block @click="ChangeItemIndex(1)" :disabled="ItemDisable('right')">
                            <v-icon>mdi-arrow-right-thick</v-icon>
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block @click="ChangeTextureIndex(-1)" :disabled="TextureDisable('left')">
                            <v-icon>mdi-arrow-left-thick</v-icon>
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-text-field class="centered-input" v-model="TextureIndex" :disabled="TextureDisable('input')"/>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block @click="ChangeTextureIndex(1)" :disabled="TextureDisable('right')">
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
        itemMax = 0;
        textureMax = 0;
        show = false;
        itemIndex = "0";
        textureIndex = "0";

        $axios: any;

        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "PED_COMPONENT_DATA":
                        if (this.Name === e.data.name) {
                            this.ItemMax = e.data.comp.Count - 1;
                            this.TextureMax = e.data.comp.TextureCount - 1;
                            this.Show = e.data.comp.HasAnyVariations;
                        }
                        break;
                    default:
                        break;
                }
            });
        }

        ChangeItemIndex(value: number) {
            let v = parseInt(this.ItemIndex);
            v += value;
            this.ItemIndex = v.toString();
        }

        ChangeTextureIndex(value: number) {
            let v = parseInt(this.TextureIndex);
            v += value;
            this.TextureIndex = v.toString();
        }

        ItemDisable(value: string) {
            if ((value === "right" && parseInt(this.ItemIndex) === this.ItemMax) || (value === "left" && parseInt(this.ItemIndex) === 0) || (value === "input" && this.ItemMax === 0)) {
                return true;
            }

            return false;
        }

        TextureDisable(value: string) {
            if ((value === "right" && parseInt(this.TextureIndex) === this.TextureMax) || (value === "left" && parseInt(this.TextureIndex) === 0) || (value === "input" && this.TextureMax === 0)) {
                return true;
            }

            return false;
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

        get ItemMax() {
            return this.itemMax;
        }

        set ItemMax(value: number) {
            this.itemMax = value;
        }

        get TextureMax() {
            return this.textureMax;
        }

        set TextureMax(value: number) {
            this.textureMax = value;
        }

        get Show() {
            return this.show;
        }

        set Show(value: boolean) {
            this.show = value;
        }

        get ItemIndex(): string {
            return this.itemIndex;
        }

        set ItemIndex(value: string) {
            let v = parseInt(value);
            if (v < 0 || isNaN(v)) {
                this.itemIndex = "0";
            } else if (v > this.ItemMax) {
                this.itemIndex = this.ItemMax.toString();
            } else {
                this.itemIndex = value;
            }

            this.TextureIndex = "0";
            this.SendComponentData();
        }

        get TextureIndex(): string {
            return this.textureIndex;
        }

        set TextureIndex(value: string) {
            let v = parseInt(value);
            if (v < 0 || isNaN(v)) {
                this.textureIndex = "0";
            } else if (v > this.TextureMax) {
                this.textureIndex = this.TextureMax.toString();
            } else {
                this.textureIndex = value;
            }

            this.SendComponentData();
        }

        SendComponentData() {
            let name = this.Name;
            let itemIndex = this.ItemIndex;
            let textureIndex = this.TextureIndex;

            this.$axios
                .post(
                    "http://framework/SET_PED_COMPONENT",
                    {
                        name,
                        itemIndex,
                        textureIndex
                    }
                )
                .catch((error: any) => {
                    console.log("error", error);
                });
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