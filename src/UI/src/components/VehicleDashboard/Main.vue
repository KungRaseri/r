<template>
    <v-container class="outter">
        <v-container class="panel">
            <v-card v-show="IsDashboardPanelActive" class="card" color="rgba(0, 0, 0, 0.5)" rounded="pill">
                <v-row justify="center">
                    <SpeedGuage class="mr-1 mt-2 mb-2" />
                    <v-container class="turn-signal">
                        <v-row justify="center">
                            <TurnSignal :id="1" icon="mdi-menu-left" />
                            <TurnSignal :id="2" icon="mdi-menu-right" />
                        </v-row>
                        <v-row justify="center">
                            <v-icon class="mr-1" :color="LightStatus()" :size="IconSize()">mdi-car-light-high</v-icon>
                            <v-icon class="ml-1" :color="BeltStatus()" :size="IconSize()">mdi-seatbelt</v-icon>
                        </v-row>
                    </v-container>
                    <RpmGuage class="ml-1 mt-2 mb-2" />
                </v-row>
            </v-card>
        </v-container>
    </v-container>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import SpeedGuage from './SpeedGuage.vue';
    import RpmGuage from './RpmGuage.vue';
    import TurnSignal from './TurnSignal.vue';

    @Component({
        components: {
            SpeedGuage,
            RpmGuage,
            TurnSignal
        },
    })
    export default class DashboardPanel extends Vue {
        isDashboardPanelActive = false;
        lights = false;
        highbeams = false;
        seatbelt = false;

        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "TOGGLE_DASHBOARD_PANEL_MODULE":
                        this.IsDashboardPanelActive = e.data.visible;
                        break;
                    case "VEHICLE_LIGHTS_MONITOR":
                        this.PanelStatus(e.data);
                        break;
                    case "SEATBELT_MONITOR":
                        this.Seatbelt = e.data._status;
                        break;
                    default:
                        break;
                }
            });
        }

        PanelStatus(value: any) {
            if (this.Lights != value._lights) {
                this.Lights = value._lights;
            }

            if (this.Highbeams != value._highbeams) {
                this.Highbeams = value._highbeams;
            }
        }

        LightStatus() {
            if (this.Highbeams) {
                return "light-blue lighten-2";
            } else if (this.Lights) {
                return "white";
            }

            return "black";
        }

        BeltStatus() {
            if (this.Seatbelt) {
                return "light-blue lighten-2";
            }

            return "black";
        }

        IconSize() {
            let size = window.innerWidth + window.innerHeight;
            return size * 0.012;
        }

        get IsDashboardPanelActive() {
            return this.isDashboardPanelActive
        }

        set IsDashboardPanelActive(value: boolean) {
            this.isDashboardPanelActive = value;
        }

        get Lights() {
            return this.lights;
        }

        set Lights(value: boolean) {
            this.lights = value;
        }

        get Highbeams() {
            return this.highbeams;
        }

        set Highbeams(value: boolean) {
            this.highbeams = value;
        }

        get Seatbelt() {
            return this.seatbelt;
        }

        set Seatbelt(value: boolean) {
            this.seatbelt = value;
        }
    }
</script>

<style>
    .fast-transition .v-progress-circular__overlay {
        transition: all 0.1s ease-in-out;
    }

    .guage-label {
        font-size: calc((1vw + 1vh));
    }

    .guage-sub-label {
        font-size: calc((1vw + 1vh) * 0.4);
        margin-top: calc(1vh * 0.3) !important;
    }

    .turn-signal {
        width: auto;
        margin: 0;
    }
</style>

<style scoped>
    .panel {
        width: 16%;
        height: 100%;
        padding: 0;
        margin-left: auto;
        margin-right: auto;
        font-weight: bold;
        color: white;
        z-index: 50;
    }

    .card {
        position: absolute;
        width: 16%;
        height: auto;
        padding: 10px;
        bottom: 5%;
    }
</style>