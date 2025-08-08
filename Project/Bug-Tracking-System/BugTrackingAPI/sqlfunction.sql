CREATE OR REPLACE FUNCTION getbugsassignedtodev(dev_id BIGINT)
RETURNS TABLE (
    "BugId" BIGINT,
    "BugName" TEXT,
    "Description" TEXT,
    "CvssScore" REAL,
    "SubmittedOn" TIMESTAMP,
    "ResolvedAt" TIMESTAMP,
    "Status" TEXT,
    "SubmittedById" BIGINT,
    "Screenshot" TEXT
)
AS $$
BEGIN
    RETURN QUERY
    SELECT 
        b."BugId"::BIGINT, 
        b."BugName"::TEXT, 
        b."Description"::TEXT, 
        b."CvssScore"::REAL,
        b."SubmittedOn"::TIMESTAMP,
        b."ResolvedAt"::TIMESTAMP,
        b."Status"::TEXT,
        b."SubmittedById"::BIGINT,
        b."Screenshot"::TEXT
    FROM public."Bugs" b
    WHERE b."BugId" IN (
        SELECT a."BugId"
        FROM public."BugAssignments" a
        WHERE a."DevId" = dev_id
    );
END;
$$ LANGUAGE plpgsql;