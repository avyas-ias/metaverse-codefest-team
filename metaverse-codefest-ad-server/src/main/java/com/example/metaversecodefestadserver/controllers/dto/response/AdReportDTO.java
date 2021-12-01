package com.example.metaversecodefestadserver.controllers.dto.response;

public class AdReportDTO {

    /**
     * Session Id
     */
    private final String sessionId;

    /**
     * View count for this session
     */
    private Integer viewCount;

    /**
     * Avg. view time for this session
     */
    private Float avgViewTime;


    public AdReportDTO(final String sessionId) {
        this.sessionId = sessionId;
        this.viewCount = 0;
        this.avgViewTime = 0f;
    }

    public void addViewInfo(final float deltaTime) {
        this.viewCount++;
        this.avgViewTime += deltaTime;
    }

    public String getSessionId() {
        return this.sessionId;
    }

    public Integer getViewCount() {
        return this.viewCount;
    }

    public Float getAvgViewTime() {
        return this.avgViewTime;
    }

    public void setViewCount(Integer viewCount) {
        this.viewCount = viewCount;
    }

    public void setAvgViewTime(Float avgViewTime) {
        this.avgViewTime = avgViewTime;
    }

    public boolean equals(final Object o) {
        if (o == this) return true;
        if (!(o instanceof AdReportDTO)) return false;
        final AdReportDTO other = (AdReportDTO) o;
        if (!other.canEqual((Object) this)) return false;
        final Object this$sessionId = this.getSessionId();
        final Object other$sessionId = other.getSessionId();
        if (this$sessionId == null ? other$sessionId != null : !this$sessionId.equals(other$sessionId)) return false;
        final Object this$viewCount = this.getViewCount();
        final Object other$viewCount = other.getViewCount();
        if (this$viewCount == null ? other$viewCount != null : !this$viewCount.equals(other$viewCount)) return false;
        final Object this$avgViewTime = this.getAvgViewTime();
        final Object other$avgViewTime = other.getAvgViewTime();
        if (this$avgViewTime == null ? other$avgViewTime != null : !this$avgViewTime.equals(other$avgViewTime))
            return false;
        return true;
    }

    protected boolean canEqual(final Object other) {
        return other instanceof AdReportDTO;
    }

    public int hashCode() {
        final int PRIME = 59;
        int result = 1;
        final Object $sessionId = this.getSessionId();
        result = result * PRIME + ($sessionId == null ? 43 : $sessionId.hashCode());
        final Object $viewCount = this.getViewCount();
        result = result * PRIME + ($viewCount == null ? 43 : $viewCount.hashCode());
        final Object $avgViewTime = this.getAvgViewTime();
        result = result * PRIME + ($avgViewTime == null ? 43 : $avgViewTime.hashCode());
        return result;
    }

    public String toString() {
        return "AdReportDTO(sessionId=" + this.getSessionId() + ", viewCount=" + this.getViewCount() + ", avgViewTime=" + this.getAvgViewTime() + ")";
    }
}
