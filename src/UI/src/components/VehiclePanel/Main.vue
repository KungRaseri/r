<template>
    <v-slide-y-reverse-transition>
        <v-card v-show="GetVehiclePanelActive" class="panel" color="rgba(0, 0, 0, 0.6)" rounded="lg">
            <v-container class="inset">
                <v-row class="top-row">
                    <v-col>
                        <v-btn block :color="GetStatus(GetEngine)" @click="ToggleComponent('engine')">
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
                        <v-btn height="100%" block :color="GetStatus(GetDFL)" @click="ToggleComponent('dfl')">
                            D
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block :color="GetStatus(GetWFL)" @click="ToggleComponent('wfl')">
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
                        <v-btn height="100%" block :color="GetStatus(GetWFR)" @click="ToggleComponent('wfr')">
                            W
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block :color="GetStatus(GetDFR)" @click="ToggleComponent('dfr')">
                            D
                        </v-btn>
                    </v-col>
                </v-row>
                <v-row class="outer-row" dense>
                    <v-col>
                        <v-btn height="100%" block :color="GetStatus(GetDBL)" @click="ToggleComponent('dbl')">
                            D
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block :color="GetStatus(GetWBL)" @click="ToggleComponent('wbl')">
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
                        <v-btn height="100%" block :color="GetStatus(GetWBR)" @click="ToggleComponent('wbr')">
                            W
                        </v-btn>
                    </v-col>
                    <v-col>
                        <v-btn height="100%" block :color="GetStatus(GetDBR)" @click="ToggleComponent('dbr')">
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
        DFL = false;
        DFR = false;
        DBL = false;
        DBR = false;

        WFL = false;
        WFR = false;
        WBL = false;
        WBR = false;
        
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
            this.GetDFL = value._dfl;
        }

        get GetVehiclePanelActive() {
            return this.isVehiclePanelActive;
        }

        set GetVehiclePanelActive(value: boolean) {
            this.isVehiclePanelActive = value;
        }

        GetStatus(value: any) {
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

        ToggleComponent(type: string) {
            let status = false;
            if (type === "engine") {
                this.GetEngine = !this.GetEngine;
                status = this.GetEngine;
            } else if (type === "dfl") {
                this.GetDFL = !this.GetDFL;
                status = this.GetDFL;
            } else if (type === "dfr") {
                this.GetDFR = !this.GetDFR;
                status = this.GetDFR;
            } else if (type === "dbl") {
                this.GetDBL = !this.GetDBL;
                status = this.GetDBL;
            } else if (type === "dbr") {
                this.GetDBR = !this.GetDBR;
                status = this.GetDBR;
            } else if (type === "wfl") {
                this.GetWFL = !this.GetWFL;
                status = this.GetWFL;
            } else if (type === "wfr") {
                this.GetWFR = !this.GetWFR;
                status = this.GetWFR;
            } else if (type === "wbl") {
                this.GetWBL = !this.GetWBL;
                status = this.GetWBL;
            } else if (type === "wbr") {
                this.GetWBR = !this.GetWBR;
                status = this.GetWBR;
            }
            this.$axios
                .post(
                    "http://framework/TOGGLE_COMPONENT",
                    { type, status }
                )
                .catch((error: any) => {
                    console.log("error", error);
                });
        }

        get GetDFL() {
            return this.DFL;
        }

        set GetDFL(value: boolean) {
            this.DFL = value;
        }

        get GetDFR() {
            return this.DFR;
        }

        set GetDFR(value: boolean) {
            this.DFR = value;
        }

        get GetDBL() {
            return this.DBL;
        }

        set GetDBL(value: boolean) {
            this.DBL = value;
        }

        get GetDBR() {
            return this.DBR;
        }

        set GetDBR(value: boolean) {
            this.DBR = value;
        }

        get GetWFL() {
            return this.WFL;
        }

        set GetWFL(value: boolean) {
            this.WFL = value;
        }

        get GetWFR() {
            return this.WFR;
        }

        set GetWFR(value: boolean) {
            this.WFR = value;
        }

        get GetWBL() {
            return this.WBL;
        }

        set GetWBL(value: boolean) {
            this.WBL = value;
        }

        get GetWBR() {
            return this.WBR;
        }

        set GetWBR(value: boolean) {
            this.WBR = value;
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