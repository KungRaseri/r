<template>
    <v-slide-y-reverse-transition>
        <v-card v-show="GetVehiclePanelActive" class="panel" color="rgba(0, 0, 0, 0.6)" rounded="lg">
            <v-container class="inset">
                <v-row class="top-row">
                    <v-col>
                        <v-btn block :color="GetStatus(GetEngine)" @click="ToggleEngine()">
                            Engine
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn block>
                            Belt
                        </v-btn>
                    </v-col>
                </v-row>
                <v-row class="outer-row" dense>
                    <v-col>
                        <v-btn height="100%" block :color="GetStatus()">
                            D
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            W
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            S
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            S
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            W
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            D
                        </v-btn>
                    </v-col>
                </v-row>
                <v-row class="outer-row" dense>
                    <v-col>
                        <v-btn height="100%" block>
                            D
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            W
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            S
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            S
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            W
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block>
                            D
                        </v-btn>
                    </v-col>
                </v-row>
            </v-container>
        </v-card>
    </v-slide-y-reverse-transition>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';

    @Component({
        components: {
        },
    })
    export default class VehiclePanel extends Vue {
        isVehiclePanelActive = false;
        $axios: any;

        Engine = false;
        
        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "TOGGLE_VEHICLE_PANEL_MODULE":
                        this.GetVehiclePanelActive = e.data.visible;
                        break;
                    case "VEHICLE_PANEL_DATA":
                        this.PanelStatus(e.data);
                        break;
                    default:
                        break;
                }
            });

            window.addEventListener("keydown", (e) => {
                if (e.key === "Escape") {
                    this.GetVehiclePanelActive = false;
                    this.ResetFocus();
                }
            });
        }

        ResetFocus() {
            this.$axios
                .post(
                    "http://framework/RESET_FOCUS",
                    {}
                )
                .catch((error: any) => {
                    console.log("error", error);
                });
        }

        PanelStatus(value: any) {
            this.GetEngine = value._engine;
        }

        get GetVehiclePanelActive() {
            return this.isVehiclePanelActive;
        }

        set GetVehiclePanelActive(value: boolean) {
            this.isVehiclePanelActive = value;
        }

        GetStatus(value: any) {
            console.log(value);
            if (value) {
                return "green";
            }
            return "";
        }

        get GetEngine() {
            return this.Engine;
        }

        set GetEngine(value: boolean) {
            this.Engine = value;
        }

        ToggleEngine() {
            this.GetEngine = !this.GetEngine;
            let engine = this.GetEngine;
            this.$axios
                .post(
                    "http://framework/TOGGLE_ENGINE",
                    { engine }
                )
                .catch((error: any) => {
                    console.log("error", error);
                });
        }
    }
</script>

<style>
    html {
        --panel-width: 25%;
    }
</style>

<style scoped>

    .panel {
        position: fixed;
        height: auto;
        width: var(--panel-width);
        bottom: 5%;
        margin-left: calc((100% - var(--panel-width)) / 2 );
        margin-right: calc((100% - var(--panel-width)) / 2);
    }

    .inset {
        height: 100%;
        padding: 10px !important;
    }

    .top-row {
        height: 20%;
        flex-wrap: nowrap;
    }

    .outer-row {
        height: 5vh;
        flex-wrap: nowrap;
    }

    .inner-spacing {
        height: var(--spacer-height);
    }

    .v-btn {
        padding: 0 !important;
    }

    .v-card {
        padding: 0 !important;
    }
</style>