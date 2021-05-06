<template>
    <v-card v-if="Show" class="panel" dark>
        <v-container class="fill">
            <v-row dense>
                <v-col>
                    <v-btn color="red" :rounded="true" block @click="goback()">Back</v-btn>
                </v-col>
                <v-col>
                    <v-btn color="green" :rounded="true" block>Continue</v-btn>
                </v-col>
            </v-row>
            <v-divider class="my-4" />
            <v-container class="scroll">
                <v-expansion-panels>
                    <v-container class="no-filler" v-if="IsFreemode()">
                        <v-subheader>Blends</v-subheader>
                        <StyleComponent name="FaceBlend" />
                        <StyleComponent name="SkinBlend" />
                    </v-container>
                    <v-container class="no-filler" v-if="IsFreemode()">
                        <v-subheader>Barber</v-subheader>
                        <StyleComponent name="Hair" :ped="Ped" :render="true" type="comps" />
                        <v-subheader>Overlays</v-subheader>
                        <StyleComponent v-for="item in Overlays" :key="'overlays:' + item" :ped="Ped" :name="item" :render="false" type="overlays" />
                    </v-container>
                    <v-subheader>Components</v-subheader>
                    <StyleComponent v-for="item in Components" :key="'comps:' + item" :ped="Ped" :name="item" :render="false" type="comps" />
                    <v-subheader>Props</v-subheader>
                    <StyleComponent v-for="item in Props" :key="'props:' + item" :ped="Ped" :name="item" :render="false" type="props" />
                </v-expansion-panels>
            </v-container>
        </v-container>
    </v-card>
</template>

<script lang="ts">
    import { Component, Vue, Prop, Emit } from 'vue-property-decorator';
    import StyleComponent from './StyleComponent.vue';

    @Component({
        components: {
            StyleComponent
        },
    })
    export default class PedCustomize extends Vue {
        @Prop(Boolean) show = this.Show;
        @Prop(String) ped = this.Ped;

        components = [""];
        props = [""];
        overlays = [""];
        $axios: any;

        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "AGGREGATE_COMPONENTS":
                        if (e.data.type === "comps") {
                            this.Components = e.data.comps;
                        } else if (e.data.type === "props") {
                            this.Props = e.data.comps;
                        } else if (e.data.type === "overlays") {
                            this.Overlays = e.data.comps;
                        }
                        break;
                    default:
                        break;
                }
            });
        }

        @Emit()
        goback() {
            return true;
        }

        IsFreemode() {
            if (this.Ped === "FreemodeFemale01" || this.Ped === "FreemodeMale01") {
                return true;
            }

            return false;
        }

        get Show() {
            return this.show;
        }

        set Show(value: boolean) {
            this.show = value;
        }

        get Components() {
            return this.components;
        }

        set Components(value: string[]) {
            this.components = value;
        }

        get Props() {
            return this.props;
        }

        set Props(value: string[]) {
            this.props = value;
        }

        get Overlays() {
            return this.overlays;
        }

        set Overlays(value: string[]) {
            this.overlays = value;
        }

        get Ped() {
            return this.ped;
        }

        set Ped(value: string) {
            this.ped = value;
        }
    }
</script>

<style scoped>
    .panel {
        position: fixed;
        top: 0;
        right: 0;
        width: 20%;
        height: 100%;
    }

    .fill {
        height: 100%;
    }

    .scroll {
        overflow-x: hidden;
        overflow-y: auto;
        height: 87%;
    }

    .no-filler {
        padding: 0;
    }

    .v-subheader {
        justify-content: center;
    }
</style>