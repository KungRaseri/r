<template>
    <v-expansion-panel v-show="ShowElement()">
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
                        <v-text-field class="centered-input" v-model="ItemIndex" :disabled="ItemDisable('input')" />
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
                        <v-text-field class="centered-input" v-model="TextureIndex" :disabled="TextureDisable('input')" />
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block @click="ChangeTextureIndex(1)" :disabled="TextureDisable('right')">
                            <v-icon>mdi-arrow-right-thick</v-icon>
                        </v-btn>
                    </v-col>
                </v-row>
                <v-row v-show="Slider" class="mt-6" dense>
                    <v-slider v-model="SliderValue" :min="Min" :max="1" :step="0.01" :thumb-label="true" thumb-color="red" />
                </v-row>
                <v-row v-show="IsFreemode() && ShowColors()">
                    <v-expansion-panels>
                        <ColorGroup title="Primary Color" :name="Name" :type="Type" target="primary"/>
                        <ColorGroup v-show="ShowSecondary()" title="Secondary Color" :name="Name" :type="Type" target="secondary" />
                    </v-expansion-panels>
                </v-row>
                <v-row v-show="Name === 'FaceBlend'">
                    <v-expansion-panels>
                        <v-expansion-panel>
                            <v-expansion-panel-header>
                                Facial Sliders
                            </v-expansion-panel-header>
                            <v-expansion-panel-content>
                                <FacialSlider v-for="item in facials" :key="'face:' + item" :name="item"/>
                            </v-expansion-panel-content>
                        </v-expansion-panel>
                    </v-expansion-panels>
                </v-row>
            </v-container>
        </v-expansion-panel-content>
    </v-expansion-panel>
</template>

<script lang="ts">
    import { Component, Vue, Prop } from 'vue-property-decorator';
    import ColorGroup from './ColorGroup.vue';
    import FacialSlider from './FacialSlider.vue';

    @Component({
        components: {
            ColorGroup,
            FacialSlider
        },
    })
    export default class StyleComponent extends Vue {
        @Prop(String) name = this.Name;
        @Prop(String) ped = this.Ped;
        @Prop(Boolean) render = this.Render;
        @Prop(String) type = this.Type;

        facials = [""];

        slider = false;
        sliderValue = 0;
        itemMax = 0;
        textureMax = 0;
        show = false;
        itemIndex = "0";
        textureIndex = "0";
        suppress = false;
        min = 0;

        $axios: any;

        constructor() {
            super();

            if (this.Type === "blends") {
                this.Suppress = true;
                this.Slider = true;
                this.Show = true;
                this.ItemMax = 45;
                this.TextureMax = 45;
                this.SliderValue = 0.5;
            } else if (this.Type === "overlays") {
                this.Slider = true;
            }
        }

        ShowElement() {
            if (!this.Show) {
                return false
            } else if (this.IsFreemode() && this.Name === "Hair" && this.Render) {
                return true;
            } else if (this.IsFreemode() && this.Name === "Hair" && !this.Render) {
                return false;
            } else if (this.IsFreemode() && this.Name === "Face") {
                return false;
            }

            return true;
        }

        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "PED_COMPONENT_DATA":
                        if (this.Name === e.data.name) {
                            if (e.data.type === "props" && e.data.comp.Index === 0) {
                                this.ItemMax = e.data.comp.Count;
                                this.TextureMax = e.data.comp.TextureCount;
                            } else {
                                this.ItemMax = e.data.comp.Count - 1;
                                this.TextureMax = e.data.comp.TextureCount - 1;
                            }

                            if (e.data.type === "overlays") {
                                this.SliderValue = e.data.comp.Value;
                            }

                            this.Show = e.data.comp.HasAnyVariations;
                        }
                        break;
                    case "PED_COMPONENT_UPDATE":
                        if (this.Name === e.data.name) {
                            if (e.data.type === "props" && e.data.comp.Index === 0) {
                                this.TextureMax = e.data.comp.TextureCount;
                            } else {
                                this.TextureMax = e.data.comp.TextureCount - 1;
                            }
                        }
                        break;
                    case "AGGREGATE_COMPONENTS":
                        if (e.data.type === "facials") {
                            this.Facials = e.data.comps;
                        }
                        break;
                    default:
                        break;
                }
            });
        }

        ShowColors() {
            if (this.Name === "Hair") {
                return true;
            } else if (this.Type === "overlays" && (this.Name === "FacialHair" || this.Name === "Eyebrows" || this.Name === "Makeup" || this.Name === "Blush" || this.Name === "Lipstick" || this.Name === "ChestHair")) {
                return true;
            }

            return false;
        }

        ShowSecondary() {
            if (this.Name === "Makeup" || this.Name === "Hair") {
                return true;
            }

            return false;
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

        IsFreemode() {
            if (this.Ped === "FreemodeFemale01" || this.Ped === "FreemodeMale01") {
                return true;
            }

            return false;
        }

        IsHasColor() {
            if (this.Name === "Hair" || this.Type === "overlays") {
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

        get Ped() {
            return this.ped;
        }

        set Ped(value: string) {
            this.ped = value;
        }

        get Render() {
            return this.render;
        }

        set Render(value: boolean) {
            this.render = value;
        }

        get Type() {
            return this.type;
        }

        set Type(value: string) {
            this.type = value;
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

            if (!this.Suppress && this.TextureIndex !== "0") {
                this.TextureIndex = "0";
            }

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

        get Suppress() {
            return this.suppress;
        }

        set Suppress(value: boolean) {
            this.suppress = value;
        }

        get Min() {
            return this.min;
        }

        set Min(value: number) {
            this.min = value;
        }

        get SliderValue() {
            return this.sliderValue;
        }

        set SliderValue(value: number) {
            this.sliderValue = value;
            this.SendComponentData();
        }
        get Facials() {
            return this.facials;
        }

        set Facials(value: string[]) {
            this.facials = value;
        }

        SendComponentData() {
            if (this.Type !== undefined) {
                let name = this.Name || "";
                let itemIndex = parseInt(this.ItemIndex).toString() || "0";
                let textureIndex = parseInt(this.TextureIndex).toString() || "0";
                let sliderValue = this.SliderValue || 0;
                let type = this.Type;

                this.$axios
                    .post(
                        "http://framework/SET_PED_COMPONENT",
                        {
                            name,
                            itemIndex,
                            textureIndex,
                            sliderValue,
                            type
                        }
                    )
                    .catch((error: any) => {
                        console.log("error", error);
                    });
            }
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