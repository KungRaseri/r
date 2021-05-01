<template>
    <v-progress-circular class="fast-transition" :size="CircleSize()" :width="CircleWidth()" color="white" :value="Ratio" rotate="90">
        <v-container>
            <v-row class="speed" justify="center">{{ Math.round(Speed) }}</v-row>
            <v-row class="speed-label" justify="center">MPH</v-row>
        </v-container>
    </v-progress-circular>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';

    @Component({
        components: {
        },
    })
    export default class CircleGuage extends Vue {
        speed = 0;
        ratio = 0;

        mounted() {
            window.addEventListener("message", (e) => {
                switch (e.data.eventName) {
                    case "VEHICLE_SPEED_MONITOR":
                        this.DataUpdate(e.data);
                        break;
                    default:
                        break;
                }
            });
        }

        DataUpdate(value: any) {
            this.Speed = value.speed;
            this.Ratio = value.ratio;
        }

        CircleSize() {
            let size = window.innerWidth + window.innerHeight;
            return size * 0.03;
        }

        CircleWidth() {
            let size = window.innerWidth + window.innerHeight;
            return size / 300;
        }

        get Speed() {
            return this.speed;
        }

        set Speed(value: number) {
            this.speed = value;
        }

        get Ratio() {
            return this.ratio;
        }

        set Ratio(value: number) {
            this.ratio = value;
        }
    }
</script>

<style>
    .fast-transition .v-progress-circular__overlay {
        transition: all 0.1s ease-in-out;
    }
</style>

<style scoped>
    .speed {
        font-size: calc((1vw + 1vh));
    }

    .speed-label {
        font-size: calc((1vw + 1vh) * 0.4);
        margin-top: calc(1vh * 0.2);
    }
</style>