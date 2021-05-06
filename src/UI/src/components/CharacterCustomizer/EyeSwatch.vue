<template>
    <v-expansion-panel>
        <v-expansion-panel-header>Eyes</v-expansion-panel-header>
        <v-expansion-panel-content>
            <v-row v-for="i in 8" :key="'rc' + i" no-gutters>
                <v-col v-for="j in 4" :key="'cc' + j + ':' + i">
                    <v-card class="eye-container mb-3" height="60" width="60" @click="ChangeColor(((i - 1) * 4 + j) - 1)">
                        <v-img class="eyes" :src="GetImage(((i - 1) * 4 + j) - 1)" />
                    </v-card>
                </v-col>
            </v-row>
        </v-expansion-panel-content>
    </v-expansion-panel>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';

    @Component({
        components: {
        },
    })
    export default class EyeSwatch extends Vue {
        $axios: any;

        GetImage(value: number) {
            return require("../../assets/eyes/" + value + ".png");
        }

        ChangeColor(value: number) {
            let name = "eye";
            let type = "eyes";
            let index = value;
            let target = "primary";

            this.$axios
                .post(
                    "http://framework/SET_COMPONENT_COLOR",
                    {
                        name,
                        type,
                        index,
                        target
                    }
                )
                .catch((error: any) => {
                    console.log("error", error);
                });
        }
    }
</script>

<style scoped>
    .eye-container {
        overflow: hidden;
    }

    .eyes {
        height: 200%;
        width: 200%;
        transform: translateY(-25%);
        overflow: hidden;
    }
</style>