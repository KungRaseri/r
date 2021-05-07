<template>
    <v-container>
        <v-subheader>{{ Name }}</v-subheader>
        <v-slider v-model="SliderValue" :min="-1" :max="1" :step="0.01" :thumb-label="true" thumb-color="red" />
    </v-container>
</template>

<script lang="ts">
    import { Component, Vue, Prop } from 'vue-property-decorator';

    @Component({
        components: {
        },
    })
    export default class FacialSlider extends Vue {
        @Prop(String) name = this.Name;

        sliderValue = 0;
        $axios: any;

        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "PED_COMPONENT_DATA":
                        if (this.Name === e.data.name) {
                            if (e.data.type === "facials") {
                                this.SliderValue = e.data.comp.Value;
                            }
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

        get SliderValue() {
            return this.sliderValue;
        }

        set SliderValue(value: number) {
            this.sliderValue = value;
            this.SendComponentData();
        }

        SendComponentData() {
            let name = this.Name || "";
            let itemIndex = "0";
            let textureIndex = "0";
            let sliderValue = this.SliderValue || 0;
            let type = "facials";

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
</script>

<style scoped>
    .v-subheader {
        height: auto;
        justify-content: center;
    }

    .v-messages {
        min-height: 0px;
    }
</style>