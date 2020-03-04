<template>
  <div class="w80 pr10 jcc aic dfc ">
    <div>
      <a href="#" @click="upvote() ">
        <i class="el-icon-caret-top fz20 " @ style="color: dodgerblue;"></i>
      </a>
    </div>
    <div class=" fz27">
      {{question.score}}
    </div>
    <div>
      <i class="el-icon-caret-bottom fz20" style="color: dodgerblue;"></i>
    </div>
  </div>
</template>
<script>
import questionHub from '../../Hub/questionHub'

export default {
  name: 'Score',
  props: {
    question: {
      type: Object,
      required: true
    }
  },
  created () {

    // Listen to score changes coming from SignalR events
    this.$queestionHub.$on('score-changed', this.onScoreChanged)
  },
  beforeDestroy () {
    // Make sure to cleanup SignalR event handlers when removing the component
    this.$queestionHub.$off('score-changed', this.onScoreChanged)
  },

  methods: {
    async upvote () {
      const { data: res } = await this.$http.patch(`/api/question/${this.question.id}/upvote`)
      Object.assign(this.question, res.data)
    },
    onScoreChanged ({ questionId, score }) {
      if (this.question.id !== questionId) return
      Object.assign(this.question, { score })
    },
  }
}
</script>
