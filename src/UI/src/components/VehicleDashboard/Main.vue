<template>
    <v-card class="panel" color="rgba(0, 0, 0, 0.5)">
        <v-container>
            <v-row justify="center">
                <CircleGuage />
            </v-row>
        </v-container>
    </v-card>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import CircleGuage from './CircleGuage.vue';

    @Component({
        components: {
            CircleGuage
        },
    })
    export default class DashboardPanel extends Vue {
    }
</script>

<style>
    html {
        --dashboard-panel-width: 50%;
    }
</style>

<style scoped>
    .panel {
        position: fixed;
        width: 50%;
        height: auto;
        bottom: 1%;
        margin-left: calc((100% - var(--dashboard-panel-width)) / 2 );
        margin-right: calc((100% - var(--dashboard-panel-width)) / 2);
        font-weight: bold;
        color: white;
    }
</style>