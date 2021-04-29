<template>
    <v-slide-y-reverse-transition>
        <v-card v-show="IsVehiclePanelActive" class="panel" color="rgba(0, 0, 0, 0.5)" rounded="lg">
            <v-container class="inset">
                <v-row class="outer-row" dense>
                    <v-col cols="4">
                        <ToggleButton :status="false" type="engine" :seat="Seat" icon="mdi-power"/>
                    </v-col>
                    <v-col cols="2">
                        <ToggleButton :status="false" type="door" :index="4" :seat="Seat" icon="mdi-car" />
                    </v-col>
                    <v-col cols="2">
                        <ToggleButton :status="false" type="door" :index="5" :seat="Seat" icon="mdi-car-back" />
                    </v-col>
                    <v-col cols="4">
                        <v-btn height="100%" block>
                            Belt
                        </v-btn>
                    </v-col>
                </v-row>
                <v-row class="outer-row" dense>
                    <v-col>
                        <ToggleButton :status="false" type="door" :index="0" :seat="Seat" icon="mdi-car-door" />
                    </v-col>
                    <v-col>
                        <ToggleButton :status="false" type="window" :index="0" :seat="Seat" icon="mdi-window-closed" />
                    </v-col>
                    <v-col>
                        <SeatButton :status="false" :index="-1" :seat="Seat" :taken="Taken[0]" icon="mdi-seat-passenger mdi-flip-h" />
                    </v-col>
                    <v-col>
                        <SeatButton :status="false" :index="0" :seat="Seat" :taken="Taken[1]" icon="mdi-seat-passenger" />
                    </v-col>
                    <v-col>
                        <ToggleButton :status="false" type="window" :index="1" :seat="Seat" icon="mdi-window-closed" />
                    </v-col>
                    <v-col>
                        <ToggleButton :status="false" type="door" :index="1" :seat="Seat" icon="mdi-car-door mdi-flip-h" />
                    </v-col>
                </v-row>
                <v-row class="outer-row" dense>
                    <v-col>
                        <ToggleButton :status="false" type="door" :index="2" :seat="Seat" icon="mdi-car-door mdi-flip-v" />
                    </v-col>
                    <v-col>
                        <ToggleButton :status="false" type="window" :index="2" :seat="Seat" icon="mdi-window-closed" />
                    </v-col>
                    <v-col>
                        <SeatButton :status="false" :index="1" :seat="Seat" :taken="Taken[2]" icon="mdi-seat-passenger mdi-rotate-180" />
                    </v-col>
                    <v-col>
                        <SeatButton :status="false" :index="2" :seat="Seat" :taken="Taken[3]" icon="mdi-seat-passenger mdi-flip-v" />
                    </v-col>
                    <v-col>
                        <ToggleButton :status="false" type="window" :index="3" :seat="Seat" icon="mdi-window-closed" />
                    </v-col>
                    <v-col>
                        <ToggleButton :status="false" type="door" :index="3" :seat="Seat" icon="mdi-car-door mdi-rotate-180" />
                    </v-col>
                </v-row>
            </v-container>
        </v-card>
    </v-slide-y-reverse-transition>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import ToggleButton from './ToggleButton.vue';
    import SeatButton from './SeatButton.vue';

    @Component({
        components: {
            ToggleButton,
            SeatButton
        },
    })
    export default class VehiclePanel extends Vue {
        isVehiclePanelActive = true;
        seat = -1;
        taken = [true, false, false, false];
        $axios: any;

        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "TOGGLE_VEHICLE_PANEL_MODULE":
                        this.isVehiclePanelActive = e.data.visible;
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
                    this.isVehiclePanelActive = false;
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
            this.Seat = value._seat;
            this.Taken = value._taken;
        }

        GetStatus(value: any) {
            if (value) {
                return "green";
            }
            return "";
        }

        ToggleComponent(type: string) {
            let status = false;
            this.$axios
                .post(
                    "http://framework/TOGGLE_COMPONENT",
                    { type, status }
                )
                .catch((error: any) => {
                    console.log("error", error);
                });
        }

        get IsVehiclePanelActive() {
            return this.isVehiclePanelActive;
        }

        set IsVehiclePanelActive(value: boolean) {
            this.isVehiclePanelActive = value;
        }

        get Seat() {
            return this.seat;
        }

        set Seat(value: number) {
            this.seat = value;
        }

        get Taken() {
            return this.taken;
        }

        set Taken(value: boolean[]) {
            this.taken = value;
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

    .button-seat-in {
        pointer-events: none;
        background-color: rgb(0 128 0 / 0.53) !important;
    }

    .button-seat {
        pointer-events: none;
        background-color: rgb(128 0 0 / 0.53) !important;
    }

    .v-card {
        padding: 0 !important;
    }
</style>